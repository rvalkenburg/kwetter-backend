using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.ProfileService.Rest.Models.Requests
{
    public class GetProfilesRequest
    {
        [Required]
        [Range(0, 100)]
        public int PageSize { get; set; }
        
        [Required]
        public int PageNumber { get; set; }
        public int Name { get; set; }
    }
}