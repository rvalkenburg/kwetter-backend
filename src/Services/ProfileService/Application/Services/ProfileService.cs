using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Profile = Kwetter.Services.ProfileService.Domain.Entity.Profile;

namespace Kwetter.Services.ProfileService.Application.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileContext _context;
        private readonly IMapper _mapper;
        private readonly INewProfileEvent _newProfileEvent;
        
        public ProfileService(IProfileContext context, IMapper mapper, INewProfileEvent newProfileEvent)
        {
            _context = context;
            _mapper = mapper;
            _newProfileEvent = newProfileEvent;
        }

        public async Task<Response<ProfileDto>> GetProfileAsync(Guid id)
        {
            Response<ProfileDto> response = new();
            var profile = await _context.Profiles.FindAsync(id);
            
            if (profile == null) return response;
            
            response.Success = true;
            response.Data = _mapper.Map<ProfileDto>(profile);

            return response;
        }

        public async Task<PaginationResponse<ProfileDto>> GetPaginatedProfiles(int pageSize, int pageNumber)
        {
            PaginationResponse<ProfileDto> response = new();

            var entities = await _context.Profiles
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (entities == null) return response;
            
            response.Data = _mapper.Map<IEnumerable<ProfileDto>>(entities);
            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.Success = true;

            return response;
        }

        public async Task<Response<ProfileDto>> CreateProfileAsync(string avatar, string displayName, string googleId, string email)
        {
            Response<ProfileDto> response = new();
            
            Profile profile = new Profile
            {
                Id = new Guid(),
                Avatar = avatar,
                DisplayName = displayName,
                DateOfCreation = DateTime.Now,
                Email = email,
                GoogleId = googleId
            };

            await _context.Profiles.AddAsync(profile);
            bool success = await _context.SaveChangesAsync() > 0;

            if (!success) return response;
            
            ProfileDto profileDto = _mapper.Map<ProfileDto>(profile);
            response.Success = true;
            response.Data = profileDto;
            //CreateProfileEvent(response.Data);
            
            return response;
        }

        public async Task<Response<ProfileDto>> UpdateProfileAsync(string displayName, string email, string description, string googleId)
        {
            Response<ProfileDto> response = new();

            Profile profile =  _context.Profiles.FirstOrDefault(x => x.GoogleId == googleId);
            
            if (profile == null) return response;
            
            profile.DisplayName = displayName;
            profile.Email = email;
            profile.Description = description;
            
            _context.Profiles.Update(profile);
            bool success = await _context.SaveChangesAsync() > 0;

            if (success)
            {
                response.Success = true;
                response.Data = _mapper.Map<ProfileDto>(profile);
                //CreateProfileEvent(response.Data);
            }
            return response;
        }

        private void CreateProfileEvent(ProfileDto profileDto)
        {
            if (profileDto != null)
            {
                _newProfileEvent.SendNewProfileEvent(profileDto);
            }
        }
    }
}