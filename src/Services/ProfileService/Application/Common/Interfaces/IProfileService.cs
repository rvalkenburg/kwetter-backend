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
        Task<Response<ProfileDto>> CreateProfileAsync(string avatar, string displayName, string googleId, string email);
        Task<Response<ProfileDto>> UpdateProfileAsync(string displayName, string email, string description, string googleId);
    }
}