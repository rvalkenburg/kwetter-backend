using System;

namespace Kwetter.Services.FollowService.Application.Common.Models
{
    public class ProfileDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }

        public string Avatar { get; set; }
    }
}