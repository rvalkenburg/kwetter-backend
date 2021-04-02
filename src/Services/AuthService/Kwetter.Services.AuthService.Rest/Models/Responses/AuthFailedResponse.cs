using System.Collections.Generic;

namespace Kwetter.Services.AuthService.Rest.Models.Responses
{
    public class AuthFailedResponse
    {
        public IEnumerable<string> errors { get; set; }

    }
}