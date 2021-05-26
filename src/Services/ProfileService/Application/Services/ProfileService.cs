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
        private readonly IProducer _producer;
        
        public ProfileService(IProfileContext context, IMapper mapper, IProducer producer)
        {
            _context = context;
            _mapper = mapper;
            _producer = producer;
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

        public async Task<PaginationResponse<ProfileDto>> GetPaginatedProfiles(int pageSize, int pageNumber, string name)
        {
            PaginationResponse<ProfileDto> response = new();

            var entities = await _context.Profiles
                .Where(x => x.DisplayName == name)
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
                UpdateProfileEvent(response.Data);
            }
            return response;
        }
        
        private void UpdateProfileEvent(ProfileDto profileDto)
        {
            if (profileDto != null)
            {
                Event<ProfileDto> updateProfileEvent = new Event<ProfileDto>
                {
                    Data = profileDto
                };
                _producer.Send("Update-Profile", updateProfileEvent);
            }
        }
    }
}