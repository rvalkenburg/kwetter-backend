using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.AuthService.Rest.Models.Requests
{
    public class AuthorizationRequest
    {
        [Required]
        public string Code { get; set; }
    }
}