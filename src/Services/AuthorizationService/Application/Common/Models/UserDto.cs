using System.Collections.Generic;

namespace Kwetter.Services.AuthorizationService.Application.Common.Models
{
    public class UserDto
    {
        public string Id { get; set; }
        public IReadOnlyDictionary<string, object> Claims { get; set; }
    }
}