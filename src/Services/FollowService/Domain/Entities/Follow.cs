using System;

namespace Kwetter.Services.FollowService.Domain.Entities
{
    public class Follow
    {
        public Guid Id { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Profile Follower { get; set; }
        public DateTime DateOfCreation { get; set; }
    }
}