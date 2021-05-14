using System;

namespace Kwetter.Services.FollowService.Application.Events
{
    public class FollowEvent
    {
        public Guid ProfileId { get; set; }
        public Guid FollowerId { get; set; }
    }
}