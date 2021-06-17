using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin.Auth;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Application.Common.Models;
using Kwetter.Services.AuthorizationService.Application.Events;
using Kwetter.Services.AuthorizationService.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.AuthorizationService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthContext _authContext;
        private readonly ILogger<AuthService> _logger;
        private readonly IMapper _mapper;
        private readonly IProducer _producer;
        private readonly ITokenVerifier _tokenVerifier;

        public AuthService(IProducer producer, IAuthContext authContext, ITokenVerifier tokenVerifier, IMapper mapper,
            ILogger<AuthService> logger)
        {
            _producer = producer;
            _authContext = authContext;
            _tokenVerifier = tokenVerifier;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<Response<UserDto>> SetUserClaims(string uid)
        {
            var response = new Response<UserDto>();

            try
            {
                var claimsDto = await _tokenVerifier.VerifyTokenAsync(uid);

                if (claimsDto == null) return response;

                var userExist = await _authContext.Users.FirstOrDefaultAsync(x =>
                    x.GoogleId == claimsDto.Claims["user_id"].ToString());
                if (userExist == null)
                {
                    var user = await CreateNewUser(claimsDto);

                    var claims = new Dictionary<string, object>
                    {
                        {"Id", user.Id},
                        {"User", true}
                    };
                    await _tokenVerifier.AddClaims(claimsDto.Subject, claims);

                    await SendNewProfileCreated(user);

                    response.Data = _mapper.Map<User, UserDto>(user);
                    response.Success = true;
                    _logger.LogInformation("Set user claims of user: " + user.Id);
                    return response;
                }
            }
            catch (FirebaseAuthException)
            {
                _logger.LogError("Claims could not be added to user!");
                throw;
            }
            catch (ArgumentNullException)
            {
                _logger.LogError("Claims could not be added to user!");
                throw;
            }

            return response;
        }

        public async Task<bool> SetAdminClaims(string uid)
        {
            var user = await _authContext.Users.FirstOrDefaultAsync(x => x.GoogleId == uid);
            var claims = new Dictionary<string, object>
            {
                {"Id", user.Id},
                {"Admin", true}
            };
            return await _tokenVerifier.AddClaims(user.GoogleId, claims);
        }

        private async Task SendNewProfileCreated(User user)
        {
            var createUserEvent = new Event<UserEvent>
            {
                Data = _mapper.Map<User, UserEvent>(user)
            };
            await _producer.Send("Create-User", createUserEvent);
        }

        private async Task<User> CreateNewUser(ClaimsDto claimsDto)
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                DisplayName = claimsDto.Claims["name"].ToString(),
                Avatar = claimsDto.Claims["picture"].ToString(),
                DateOfCreation = DateTime.Now,
                GoogleId = claimsDto.Claims["user_id"].ToString(),
                Email = claimsDto.Claims["email"].ToString()
            };

            _authContext.Users.Add(user);
            await _authContext.SaveChangesAsync();
            return user;
        }
    }
}