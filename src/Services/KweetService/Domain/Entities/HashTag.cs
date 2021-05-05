using System;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class HashTag
    {
        public Guid Id { get; set; }
        public string Tag { get; set; }
        public virtual Kweet Kweet { get; set; }
    }
}