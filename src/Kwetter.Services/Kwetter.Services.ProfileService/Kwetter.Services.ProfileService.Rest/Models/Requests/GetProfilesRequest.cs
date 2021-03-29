namespace Kwetter.Services.ProfileService.Rest.Models.Requests
{
    public class GetProfilesRequest
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public int Name { get; set; }
    }
}