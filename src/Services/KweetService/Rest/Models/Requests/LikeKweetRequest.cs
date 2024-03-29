﻿using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.KweetService.Rest.Models.Requests
{
    public class LikeKweetRequest
    {
        [Required]
        [RegularExpression("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
        public string ProfileId { get; set; }
        [Required]
        [RegularExpression("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
        public string KweetId { get; set; }
    }
}