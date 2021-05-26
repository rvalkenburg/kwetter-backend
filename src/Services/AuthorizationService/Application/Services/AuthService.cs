using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using AutoMapper;
using FirebaseAdmin.Auth;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Application.Common.Models;
using Kwetter.Services.AuthorizationService.Application.Events;
using Kwetter.Services.AuthorizationService.Domain.Entities;

namespace Kwetter.Services.AuthorizationService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IProducer _producer;
        private readonly IAuthContext _authContext;
        private readonly ITokenVerifier _tokenVerifier;
        private readonly IMapper _mapper;
        public AuthService(IProducer producer, IAuthContext authContext, ITokenVerifier tokenVerifier, IMapper mapper)
        {
            _producer = producer;
            _authContext = authContext;
            _tokenVerifier = tokenVerifier;
            _mapper = mapper;
        }
        public async Task<Response<UserDto>> SetUserClaims(string uid)
        {
            Response<UserDto> response = new Response<UserDto>();
            
            try
            {
                ClaimsDto claimsDto = await _tokenVerifier.VerifyTokenAsync(uid);
                
                if (claimsDto == null) return response;
                
                User userExist = await _authContext.Users.FirstOrDefaultAsync(x =>
                    x.GoogleId == claimsDto.Claims["user_id"].ToString());
                if (userExist == null)
                {
                    User user = new User
                    {
                        Id = new Guid(),
                        DisplayName =  claimsDto.Claims["name"].ToString(),
                        Avatar = claimsDto.Claims["picture"].ToString(), 
                        DateOfCreation = DateTime.Now,
                        GoogleId = claimsDto.Claims["user_id"].ToString(),
                        Email = claimsDto.Claims["email"].ToString(),
                    };

                    _authContext.Users.Add(user);
                    await _authContext.SaveChangesAsync();
                    
                    Dictionary<string, object> claims = new Dictionary<string, object>()
                    {
                        {"Id", user.Id },
                        { "user", true },
                    };
                    await _tokenVerifier.AddClaims(claimsDto.Subject, claims);

                    await SendNewProfileCreated(user);
                    
                    response.Data = _mapper.Map<User, UserDto>(user);
                    response.Success = true;
                    return response;
                }
            }
            catch (FirebaseAuthException  e)
            {
                Console.WriteLine(e);
                throw;
            }
            catch (ArgumentNullException  e)
            {
                Console.WriteLine(e);
                throw;
            }

            return response;
        }

        private async Task SendNewProfileCreated(User user)
        {
            Event<UserEvent> createUserEvent = new Event<UserEvent>
            {
                Data = _mapper.Map<User, UserEvent>(user)
            };
            await _producer.Send("Create-User", createUserEvent);

        }
    }
}