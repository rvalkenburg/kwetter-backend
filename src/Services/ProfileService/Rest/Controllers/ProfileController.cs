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
        
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _profileService.GetProfileAsync(new Guid(id));
            return response.Success ? new OkObjectResult(response.Data) : new NotFoundResult();
        }
        
        [HttpGet("search")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaginated([FromQuery] GetProfilesRequest profilesRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _profileService.GetPaginatedProfiles(profilesRequest.PageSize, profilesRequest.PageNumber, profilesRequest
                    .Name);
                return response.Success ? new OkObjectResult(response) : new NotFoundResult();
            }

            return StatusCode(500);
        }
        
        [HttpPut("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateProfileRequest updateProfileRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _profileService.UpdateProfileAsync(updateProfileRequest.DisplayName,
                    updateProfileRequest.Email, updateProfileRequest.Description, updateProfileRequest.GoogleId);

                return response.Success ? new OkObjectResult(response.Data) : StatusCode(500);
            }
            return StatusCode(500);
        }
    }
}