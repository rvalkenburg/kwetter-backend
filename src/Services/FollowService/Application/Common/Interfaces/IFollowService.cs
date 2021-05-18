﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Common.Models;

namespace Kwetter.Services.FollowService.Application.Common.Interfaces
{
    public interface IFollowService
    {
        Task<Response<FollowDto>> CreateFollow(Guid profileId, Guid followerId);
        Task<Response<FollowDto>> DeleteFollow(Guid profileId, Guid followerId);
        Task<Response<FollowDto>> GetStatusBetweenProfiles(Guid userId, Guid profileId);
        Task<Response<IEnumerable<FollowDto>>> GetPaginatedFollowersByProfileId(Guid id);
        Task<Response<IEnumerable<FollowingDto>>> GetPaginatedFollowingByProfileId(Guid id);

    }
}