using System;

namespace Kwetter.Services.KweetService.Application.Events
{
    public class ProfileEvent : BaseEvent
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
    }
}