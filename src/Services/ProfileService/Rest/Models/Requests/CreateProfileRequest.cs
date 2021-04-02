using System;

namespace Kwetter.Services.ProfileService.Rest.Models.Requests
{
    public class CreateProfileRequest
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
    }
}