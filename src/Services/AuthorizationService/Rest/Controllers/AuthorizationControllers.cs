using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Kwetter.Services.AuthorizationService.Rest.Controllers
{
    public class AuthorizationControllers : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthorizationControllers(IAuthService authService)
        {
            _authService = authService;
        }
    }
}