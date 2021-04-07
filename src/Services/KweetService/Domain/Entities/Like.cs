using System;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class Like
    {
        public Guid Id { get; set; }
        public Guid ProfileId { get; set; }
        public Guid KweetId { get; set; }
    }
}