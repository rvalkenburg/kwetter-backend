using System;
using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Common.Models;

namespace Kwetter.Services.FollowService.Application.Common.Interfaces
{
    public interface IFollowService
    {
        Task<Response<FollowDto>> CreateFollow(Guid profileId, Guid followerId);
        Task<Response<FollowDto>> DeleteFollow(Guid profileId, Guid followerId);
    }
}