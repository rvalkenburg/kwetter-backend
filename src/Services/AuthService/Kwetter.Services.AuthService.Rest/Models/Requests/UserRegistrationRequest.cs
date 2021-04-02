using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Kwetter.Services.AuthService.Rest.Models.Requests
{
    public class UserRegistrationRequest
    {
        [EmailAddress]
        public string Email { get; set; }

        [NotNull]
        [MinLength(8)]
        public string Password { get; set; }
    }
}