using System;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface ILikeService
    {
        Task<Response<LikeDto>> CreateLikeAsync(Guid profileId, Guid kweetId);
        Task<Response<LikeDto>> DeleteLikeAsync(Guid id);

    }
}