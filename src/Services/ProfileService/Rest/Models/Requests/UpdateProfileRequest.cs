using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.ProfileService.Rest.Models.Requests
{
    public class UpdateProfileRequest
    {
        [Required]
        [StringLength(250)]
        public string DisplayName { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public string Description { get; set; }
        public string GoogleId { get; set; }
    }
}