using System;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface ILikeService
    {
        Task<Response<int>> CreateLikeAsync(Guid profileId, Guid kweetId);
        Task<Response<int>> DeleteLikeAsync(Guid profileId, Guid kweetId);

    }
}