using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Domain.Entities;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IProfileService
    {
        Task<bool> AddOrUpdateProfile(ProfileDto profileDto);
    }
}