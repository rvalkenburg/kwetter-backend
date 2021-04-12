using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IProfileService 
    {
        void AddProfile(ProfileDto profileDto);
    }
}