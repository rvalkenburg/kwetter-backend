using System;

namespace Kwetter.Services.ProfileService.Domain.Entity
{
    public class Profile
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
    }
}