using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Common.Models;

namespace Kwetter.Services.FollowService.Application.Common.Interfaces
{
    public interface IProfileService
    {
        Task<bool> AddOrUpdateProfile(ProfileDto profileDto);
    }
}