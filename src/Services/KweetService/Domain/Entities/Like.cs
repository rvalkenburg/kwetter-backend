﻿using System;

namespace Kwetter.Services.KweetService.Domain.Entities
{
    public class Like
    {
        public Guid Id { get; set; }
        public Profile ProfileId { get; set; }
        public Kweet KweetId { get; set; }
    }
}