using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.SearchService.Rest.Models.Requests
{
    public class GetProfilesRequest
    {
        [Required]
        [Range(0, 100)]
        public int PageSize { get; set; }
        
        [Required]
        public int PageNumber { get; set; }
        
        [Required]
        public string Id { get; set; }
        
        public string Name { get; set; }
    }
}