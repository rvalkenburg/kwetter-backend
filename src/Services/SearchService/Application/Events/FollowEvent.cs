using System;

namespace Kwetter.Services.SearchService.Application.Events
{
    public class FollowEvent
    {
        public Guid ProfileId { get; set; }
        public Guid FollowerId { get; set; }  
    }
}