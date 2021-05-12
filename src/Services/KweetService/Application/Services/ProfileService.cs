using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Application.Events;
using Kwetter.Services.KweetService.Domain.Entities;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IKweetContext _context;
        
        public ProfileService(IKweetContext context)
        {
            _context = context;
        }
        public async Task<bool> AddOrUpdateProfile(ProfileEvent profileEvent)
        {
            if (profileEvent == null) return false;
            
            Profile profile = await _context.Profiles.FindAsync(profileEvent.Id);

            if (profile == null)
            {
                return await AddProfile(profileEvent);
            }

            return await UpdateProfile(profile, profileEvent);
        }

        private async Task<bool> UpdateProfile(Profile profile, ProfileEvent profileEvent)
        {
            profile.Avatar = profileEvent.Avatar;
            profile.DisplayName = profileEvent.DisplayName;
            _context.Profiles.Update(profile);
            return await _context.SaveChangesAsync() > 0;
        }
        
        private async Task<bool> AddProfile(ProfileEvent profileEvent)
        {
            Profile profile = new Profile
            {
                Id = profileEvent.Id,
                DisplayName = profileEvent.DisplayName,
                Avatar = profileEvent.Avatar
            };
            
            _context.Profiles.Add(profile);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}