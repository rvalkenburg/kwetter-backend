using System;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string DisplayName { get; set; }

        public string Avatar { get; set; }
    }
}