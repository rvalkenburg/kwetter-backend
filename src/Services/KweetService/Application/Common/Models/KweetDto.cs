using System;
using System.Collections.Generic;
using Kwetter.Services.KweetService.Domain.Entities;

namespace Kwetter.Services.KweetService.Application.Common.Models
{
    public class KweetDto
    {
        public Guid Id { get; set; }
        public ProfileDto Profile { get; set; }
        public string Message { get; set; }
        public DateTime DateOfCreation { get; set; }

        public IEnumerable<LikeDto> Likes { get; set; }
    }
}