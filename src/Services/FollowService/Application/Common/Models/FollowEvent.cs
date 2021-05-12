using System;

namespace Kwetter.Services.FollowService.Application.Common.Models
{ 
    public class FollowEvent
    {
        public Guid ProfileId { get; set; }
        public Guid FollowerId { get; set; }
    }
}