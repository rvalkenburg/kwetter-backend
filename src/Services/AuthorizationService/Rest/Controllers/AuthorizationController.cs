using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.AuthorizationService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthorizationController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthorizationController> _logger;

        public AuthorizationController(IAuthService authService, ILogger<AuthorizationController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] AddClaims createProfileRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _authService.SetUserClaims(createProfileRequest.Jwt);
                return response.Success ? new OkObjectResult(response.Data) : StatusCode(500);
            }

            return StatusCode(400);
        }
    }
}