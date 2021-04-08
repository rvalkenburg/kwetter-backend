using System;
using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.ProfileService.Rest.Models.Requests
{
    public class CreateProfileRequest
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(250)]
        public string DisplayName { get; set; }
        public string Description { get; set; }
        [Required]
        public string Avatar { get; set; }
    }
}