using System;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.KweetService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
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
            var response = await _likeService.CreateLikeAsync(new Guid(likeKweetRequest.ProfileId),
                new Guid(likeKweetRequest.KweetId));
            return response.Success ? new OkObjectResult(response) : StatusCode(500);
        }
        
        [HttpDelete("{likeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(string likeId)
        {
            var response = await _likeService.DeleteLikeAsync(new Guid(likeId));
            return response.Success ? new OkObjectResult(response) : StatusCode(500);
        }
    }
}