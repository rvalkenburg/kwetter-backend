using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Rest.Models.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.AuthorizationService.Rest.Controllers
{
    public class AuthorizationControllers : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationControllers(IAuthService authService)
        {
            _authService = authService;
        }
        
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AddClaims createProfileRequest)
        {
            if (ModelState.IsValid)
            {

            }
            return StatusCode(500);
        }
    }
}