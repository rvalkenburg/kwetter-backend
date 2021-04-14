using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;
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
        public void AddProfile(ProfileDto profileDto)
        {
            Profile profile = new Profile
            {
                Id = profileDto.Id,
                DisplayName = profileDto.DisplayName,
                Avatar = profileDto.Avatar
            };
            _context.Profiles.Add(profile);
            _context.SaveChangesAsync();
        }
    }
}