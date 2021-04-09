using System;
using System.Collections;
using System.Collections.Generic;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class Kweet
    {
        public Guid Id { get; set; }
        public virtual Profile Profile { get; set; }
        public string Message { get; set; }

        public DateTime DateOfCreation { get; set; }

        public virtual IEnumerable<Like> Likes { get; set; }
    }
}