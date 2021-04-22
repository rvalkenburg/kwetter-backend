using System.Threading.Tasks;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Application.Common.Models;

namespace Kwetter.Services.AuthService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthHttpRequest _authHttpRequest;

        public AuthService(IAuthHttpRequest authHttpRequest)
        {
            _authHttpRequest = authHttpRequest;
        }
        
        public async Task<Response<AuthResponseDto>> AuthorizeAsync(string code)
        {
            Response<AuthResponseDto> response = await _authHttpRequest.SendAuthRequest(code);
            return response;

            //check if users exists in db
            // if not, add
            // else return auth
        }
    }
}