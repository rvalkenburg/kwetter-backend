﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        
        public ProfileService(IProfileContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
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

        public async Task<Response<IEnumerable<ProfileDto>>> GetPaginatedProfiles(int pageSize, int pageNumber)
        {
            Response<IEnumerable<ProfileDto>> response = new();

            var entities = await _context.Profiles
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (entities == null) return response;
            
            response.Data = _mapper.Map<IEnumerable<ProfileDto>>(entities);
            response.Success = true;

            return response;
        }

        public async Task<Response<ProfileDto>> CreateProfileAsync(Guid id, string avatar, string description, string displayName)
        {
            Response<ProfileDto> response = new();
            
            Profile profile = new Profile
            {
                Id = id,
                Avatar = avatar,
                Description = description,
                DisplayName = displayName,
            };

            await _context.Profiles.AddAsync(profile);
            bool success = await _context.SaveChangesAsync() > 0;

            if (!success) return response;
            
            response.Success = true;
            response.Data = _mapper.Map<ProfileDto>(profile);

            return response;
        }
    }
}