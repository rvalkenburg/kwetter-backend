using System;
using Kwetter.Services.KweetService.Domain.Entities;

namespace Kwetter.Services.KweetService.Application.Common.Models
{
    public class LikeDto
    {
        public ProfileDto Profile { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}