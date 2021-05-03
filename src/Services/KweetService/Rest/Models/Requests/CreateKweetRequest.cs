using System;

namespace Kwetter.Services.KweetService.Rest.Models.Requests
{
    public class CreateKweetRequest
    {
        public string ProfileId { get; set; }
        public string Message { get; set; }
    }
}