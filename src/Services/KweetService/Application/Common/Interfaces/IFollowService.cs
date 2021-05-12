using System;
using System.Threading.Tasks;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IFollowService
    {
        Task<bool> AddOrDeleteFollow(Guid profileId, Guid followerId);
    }
}