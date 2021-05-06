﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IKweetService
    {
        Task<Response<KweetDto>> CreateKweetAsync(Guid profileId, string message);
        Task<Response<IEnumerable<KweetDto>>> GetPaginatedKweetsByProfile(int pageNumber, int pageSize, Guid profileId);
        Task<Response<IEnumerable<KweetDto>>> GetPaginatedTrendingKweets(int pageNumber, int pageSize);
    }
}