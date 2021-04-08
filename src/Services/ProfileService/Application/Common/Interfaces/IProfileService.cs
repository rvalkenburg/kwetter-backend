using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Kwetter.Services.ProfileService.Application.Common.Models;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces
{
    public interface IProfileService
    {
        Task<Response<ProfileDto>> GetProfileAsync(Guid id);
        Task<PaginationResponse<ProfileDto>> GetPaginatedProfiles(int pageSize, int pageNumber);
        Task<Response<ProfileDto>> CreateProfileAsync(Guid id, string avatar, string description, string displayName);
    }
}