﻿using System;
using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.FollowService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class FollowController : ControllerBase
    {
        private readonly IFollowService _followService;

        public FollowController(IFollowService followService)
        {
            _followService = followService;
        }
        
        [HttpPost("")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateFollowRequest createFollowRequest)
        {
            if (ModelState.IsValid)
            {
                var response = await _followService.CreateFollow(new Guid(createFollowRequest.ProfileId),
                    new Guid(createFollowRequest.FollowId));
                return response.Success ? new OkObjectResult(response) : StatusCode(500);
            }
            return StatusCode(400);
        }
        
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete([FromQuery] string id)
        {
            if (ModelState.IsValid)
            {
                var response = await _followService.DeleteFollow(new Guid(id));
                return response.Success ? new OkObjectResult(response) : StatusCode(500);
            }
            return StatusCode(400);
        }
    }
}