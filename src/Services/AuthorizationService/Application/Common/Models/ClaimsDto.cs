using System.Collections.Generic;

namespace Kwetter.Services.AuthorizationService.Application.Common.Models
{
    public class ClaimsDto
    {
        public string Subject { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public long ExpirationTimeSeconds { get; set; }
        public long IssuedAtTimeSeconds { get; set; }
        public IReadOnlyDictionary<string, object> Claims { get; set; }
    }
}