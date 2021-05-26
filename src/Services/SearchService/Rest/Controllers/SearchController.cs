using System;
using System.Threading.Tasks;
using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.SearchService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class SearchController : ControllerBase
    {
        private readonly ISearchService _searchService;

        public SearchController(ISearchService searchService)
        {
            _searchService = searchService;
        }
        
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPaginated([FromQuery] GetProfilesRequest profilesRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _searchService.GetPaginatedSearch(profilesRequest.PageSize, profilesRequest.PageNumber, profilesRequest
                    .Name, new Guid(profilesRequest.Id));
                return response.Success ? new OkObjectResult(response) : new NotFoundResult();
            }

            return StatusCode(500);
        }
    }
}