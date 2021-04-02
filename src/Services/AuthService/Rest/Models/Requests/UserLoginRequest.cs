using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kwetter.Services.AuthService.Rest.Models.Requests
{
    public class UserLoginRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [NotNull]
        public string Password { get; set; }
    }
}