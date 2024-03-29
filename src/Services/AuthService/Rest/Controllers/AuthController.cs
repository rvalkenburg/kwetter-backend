﻿using System.Threading.Tasks;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Rest.Models.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.AuthService.Rest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [AllowAnonymous]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("")]
        public async Task<IActionResult> Register([FromBody] AuthorizationRequest authorizationRequest)
        {
            if (!ModelState.IsValid) return BadRequest();
            var response = await _authService.AuthorizeAsync(authorizationRequest.Code);
            return response.Success ? new OkObjectResult(response) : StatusCode(500);
        }
    }
}