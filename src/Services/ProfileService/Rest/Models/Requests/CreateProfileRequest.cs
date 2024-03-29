﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.ProfileService.Rest.Models.Requests
{
    public class CreateProfileRequest
    {
        public string GoogleId { get; set; }
        [Required]
        [StringLength(250)]
        public string DisplayName { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Avatar { get; set; }
    }
}