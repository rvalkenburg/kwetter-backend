using System;
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
    public class LikeController : ControllerBase
    {
        private readonly ILikeService _likeService;

        public LikeController(ILikeService likeService)
        {
            _likeService = likeService;
        }
        
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] LikeKweetRequest likeKweetRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _likeService.CreateLikeAsync(new Guid(likeKweetRequest.ProfileId),
                    new Guid(likeKweetRequest.KweetId));
                return response.Success ? new OkObjectResult(response) : StatusCode(500);
            }
            return StatusCode(500);
        }
        
        [HttpDelete("{profileId}/{kweetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string profileId, string kweetId)
        {
            var response = await _likeService.DeleteLikeAsync(new Guid(profileId), new Guid(kweetId));
            return response.Success ? new OkObjectResult(response) : StatusCode(500);
        }
    }
}