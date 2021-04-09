using System;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class Like
    {
        public Guid Id { get; set; }
        public virtual Profile Profile { get; set; }
        public virtual Kweet Kweet { get; set; }
        
        public DateTime DateOfCreation { get; set; }
    }
}