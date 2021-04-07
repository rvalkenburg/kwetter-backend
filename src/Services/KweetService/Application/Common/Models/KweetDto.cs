using System;

namespace Kwetter.Services.KweetService.Application.Common.Models
{
    public class KweetDto
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public string Message { get; set; }
    }
}