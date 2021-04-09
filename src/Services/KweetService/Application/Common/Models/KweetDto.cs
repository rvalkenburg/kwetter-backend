﻿using System;
using System.Collections.Generic;
using Kwetter.Services.KweetService.Domain.Entities;

namespace Kwetter.Services.KweetService.Application.Common.Models
{
    public class KweetDto
    {
        public Guid Id { get; set; }
        public Profile ProfileId { get; set; }
        public string Message { get; set; }

        public IEnumerable<Like> Likes { get; set; }
    }
}