using System;

namespace Kwetter.Services.KweetService.Application.Common.Models
{
    public class FollowDto
    {
        public Guid ProfileId { get; set; }
        public Guid FollowerId { get; set; }
    }
}