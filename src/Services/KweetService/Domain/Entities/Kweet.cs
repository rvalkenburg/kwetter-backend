using System;
using System.Collections;
using System.Collections.Generic;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class Kweet
    {
        public Guid Id { get; set; }
        public Profile ProfileId { get; set; }
        public string Message { get; set; }

        public IEnumerable<Like> Likes { get; set; }
    }
}