using System.Threading.Tasks;
using Kwetter.Services.AuthService.Rest.Interfaces;
using Kwetter.Services.AuthService.Rest.Models.Requests;
using Kwetter.Services.AuthService.Rest.Models.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.AuthService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserRegistrationRequest userRegistrationRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var authResponse =
                await _authService.RegisterAsync(userRegistrationRequest.Email, userRegistrationRequest.Password);

            return authResponse.Success
                ? new OkObjectResult(new AuthSuccessResponse
                {
                    Token = authResponse.Token
                })
                : new BadRequestObjectResult(new AuthFailedResponse
                {
                    errors = authResponse.Errors
                });
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLoginRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var authResponse =
                await _authService.LoginAsync(userLoginRequest.Email, userLoginRequest.Password);

            return authResponse.Success
                ? new OkObjectResult(new AuthSuccessResponse
                {
                    Token = authResponse.Token
                })
                : new BadRequestObjectResult(new AuthFailedResponse
                {
                    errors = authResponse.Errors
                });
        }
    }
}