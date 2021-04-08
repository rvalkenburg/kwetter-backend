using System;
using System.Threading.Tasks;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.ProfileService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        
        [HttpGet("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
        {
            var response = await _profileService.GetProfileAsync(id);
            return response.Success ? new OkObjectResult(response.Data) : new NotFoundResult();
        }
        
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaginated(GetProfilesRequest profilesRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _profileService.GetPaginatedProfiles(profilesRequest.PageSize, profilesRequest.PageNumber);
                return response.Success ? new OkObjectResult(response) : new NotFoundResult();
            }

            return StatusCode(500);
        }
        
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateProfileRequest createProfileRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _profileService.CreateProfileAsync(createProfileRequest.Id, createProfileRequest.Avatar,
                    createProfileRequest.Description, createProfileRequest.DisplayName);

                return response.Success ? new OkObjectResult(response.Data) : StatusCode(500);
            }
            return StatusCode(500);
        }
    }
}