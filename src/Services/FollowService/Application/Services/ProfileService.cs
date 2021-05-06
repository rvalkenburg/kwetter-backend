using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.Common.Models;
using Kwetter.Services.FollowService.Domain.Entities;

namespace Kwetter.Services.FollowService.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IFollowContext _context;
        
        public ProfileService(IFollowContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrUpdateProfile(ProfileDto profileDto)
        {
            if (profileDto == null) return false;
            
            Profile profile = await _context.Profile.FindAsync(profileDto.Id);

            if (profile == null)
            {
                return await AddProfile(profileDto);
            }

            return await UpdateProfile(profile, profileDto);
        }

        private async Task<bool> UpdateProfile(Profile profile, ProfileDto profileDto)
        {
            profile.Avatar = profileDto.Avatar;
            profile.DisplayName = profileDto.DisplayName;
            _context.Profile.Update(profile);
            return await _context.SaveChangesAsync() > 0;
        }
        
        private async Task<bool> AddProfile(ProfileDto profileDto)
        {
            Profile profile = new Profile
            {
                Id = profileDto.Id,
                DisplayName = profileDto.DisplayName,
                Avatar = profileDto.Avatar
            };
            
            _context.Profile.Add(profile);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}