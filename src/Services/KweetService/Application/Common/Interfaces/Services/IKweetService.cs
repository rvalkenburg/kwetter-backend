﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces.Services
{
    public interface IKweetService
    {
        Task<Response<KweetDto>> CreateKweetAsync(Guid profileId, string message);
        Task<bool> DeleteKweetAsync(Guid kweetId);
        Task<Response<IEnumerable<KweetDto>>> GetPaginatedKweetsByProfile(int pageNumber, int pageSize, Guid profileId);
        Task<Response<IEnumerable<KweetDto>>> GetPaginatedTimeline(int pageNumber, int pageSize, Guid profileId);
    }
}