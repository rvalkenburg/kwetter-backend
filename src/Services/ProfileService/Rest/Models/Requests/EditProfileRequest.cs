namespace Kwetter.Services.ProfileService.Rest.Models.Requests
{
    public class EditProfileRequest
    {
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string Avatar { get; set; }
    }
}