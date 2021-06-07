using System;

namespace Kwetter.Services.AuthorizationService.Application.Events
{
    public class UserEvent
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
        public DateTime DateOfCreation { get; set; }
        public string Email { get; set; }
        public string GoogleId { get; set; }
    }
}