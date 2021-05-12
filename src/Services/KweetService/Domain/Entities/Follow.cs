using System;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class Follow
    {
        public Guid Id { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Profile Follower { get; set; }
    }
}