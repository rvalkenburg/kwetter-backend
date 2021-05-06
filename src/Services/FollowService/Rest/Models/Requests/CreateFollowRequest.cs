using System.ComponentModel.DataAnnotations;

namespace Kwetter.Services.FollowService.Rest.Models.Requests
{
    public class CreateFollowRequest
    {
        [Required]
        [RegularExpression("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
        public string ProfileId { get; set; }
        
        [Required]
        [RegularExpression("^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$")]
        public string FollowId { get; set; }
    }
}