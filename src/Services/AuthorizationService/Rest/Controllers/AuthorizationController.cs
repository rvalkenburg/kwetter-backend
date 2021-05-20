﻿using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.AuthorizationService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthorizationController: ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationController(IAuthService authService)
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
                await _authService.SetUserClaims(createProfileRequest.Id);
            }
            return StatusCode(500);
        }
    }
}