using System;
using Kwetter.Services.KweetService.Domain.Entities;

namespace Kwetter.Services.KweetService.Application.Common.Models
{
    public class LikeDto
    {
        public Guid Id { get; set; }
        public Profile ProfileId { get; set; }
        public Kweet KweetId { get; set; }
    }
}