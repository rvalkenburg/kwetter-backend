using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Application.Common.Models;
using Kwetter.Services.AuthService.Domain;

namespace Kwetter.Services.AuthService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthHttpRequest _authHttpRequest;
        private readonly IAuthContext _authContext;
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;

        public AuthService(IAuthHttpRequest authHttpRequest, IAuthContext authContext)
        {
            _authHttpRequest = authHttpRequest;
            _authContext = authContext;
            _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();;
        }
        
        public async Task<Response<AuthResponseDto>> AuthorizeAsync(string code)
        {
            Response<AuthResponseDto> response = await _authHttpRequest.SendAuthRequest(code);
            if (response.Success)
            {
                JwtSecurityToken token = _jwtSecurityTokenHandler.ReadJwtToken(response.Data.IdToken);
                string openId = token.Subject;
                User user = _authContext.Users.FirstOrDefault(x => x.OpenId == openId);

                if (user == null)
                {
                    user = new User
                    {
                        Id = new Guid(),
                        Avatar = token.Claims.First(x => x.Type == "picture").Value,
                        Name = token.Claims.First(x => x.Type == "given_name").Value,
                        OpenId = openId
                    };
                    _authContext.Users.Add(user);
                    await _authContext.SaveChangesAsync();
                }

                response.Data.UserId = user.Id;

            }
            return response;
        }
    }
}