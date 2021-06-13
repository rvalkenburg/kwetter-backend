using System;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Services;
using Kwetter.Services.KweetService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.KweetService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class KweetController : ControllerBase
    {
        private readonly IKweetService _kweetService;

        public KweetController(IKweetService kweetService)
        {
            _kweetService = kweetService;
        }

        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateKweetRequest createProfileRequest)
        {
            if (!ModelState.IsValid) return StatusCode(500);
            var userId = Guid.Parse(HttpContext.User.Claims.FirstOrDefault(x => x.Type == "Id").Value);

            if (userId != new Guid(createProfileRequest.ProfileId)) return StatusCode(403);

            var response = await _kweetService.CreateKweetAsync(new Guid(createProfileRequest.ProfileId),
                createProfileRequest.Message);
            return response.Success ? new OkObjectResult(response) : StatusCode(500);
        }

        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaginated([FromQuery] GetKweetsRequest getKweetsRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _kweetService.GetPaginatedKweetsByProfile(getKweetsRequest.PageNumber,
                    getKweetsRequest.PageSize, new Guid(getKweetsRequest.ProfileId));
                return response.Success ? new OkObjectResult(response) : new NotFoundResult();
            }

            return StatusCode(500);
        }

        [HttpGet("timeline")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaginatedTimline([FromQuery] GetKweetsRequest getKweetsRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _kweetService.GetPaginatedTimeline(getKweetsRequest.PageNumber,
                    getKweetsRequest.PageSize, new Guid(getKweetsRequest.ProfileId));
                return response.Success ? new OkObjectResult(response) : new NotFoundResult();
            }

            return StatusCode(500);
        }
    }
}