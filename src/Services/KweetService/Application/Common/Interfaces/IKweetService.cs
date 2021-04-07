using System;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IKweetService
    {
        Task<Response<KweetDto>> CreateKweetAsync(Guid profileId, string message);
    }
}