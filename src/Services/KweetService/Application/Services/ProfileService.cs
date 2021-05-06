﻿using System.Threading.Tasks;
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
        public async Task<bool> AddOrUpdateProfile(ProfileDto profileDto)
        {
            if (profileDto == null) return false;
            
            Profile profile = await _context.Profiles.FindAsync(profileDto.Id);

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
            _context.Profiles.Update(profile);
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
            
            _context.Profiles.Add(profile);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}