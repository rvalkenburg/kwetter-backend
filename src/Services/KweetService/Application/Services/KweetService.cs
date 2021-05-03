using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Profile = Kwetter.Services.KweetService.Domain.Entities.Profile;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class KweetService : IKweetService
    {
        private readonly IKweetContext _context;
        private readonly IMapper _mapper;
        
        public KweetService(IKweetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<KweetDto>> CreateKweetAsync(Guid profileId, string message)
        {
            Response<KweetDto> response = new Response<KweetDto>();
            Profile profile = await _context.Profiles.FindAsync(profileId);

            if (profile == null) return response;
            
            var kweet = new Kweet
            {
                Id = Guid.NewGuid(),
                Profile = profile,
                Message = message,
                DateOfCreation = DateTime.Now,
            };
            
            await _context.Kweets.AddAsync(kweet);
            bool success = await _context.SaveChangesAsync() > 0;

            if (success)
            {
                response.Success = true;
                response.Data = _mapper.Map<KweetDto>(kweet);
            }

            return response;
        }

        public async Task<Response<IEnumerable<KweetDto>>> GetPaginatedKweetsByProfile(int pageNumber, int pageSize, Guid profileId)
        {
            
            Response<IEnumerable<KweetDto>> response = new();
            
            Profile profile = await _context.Profiles.FindAsync(profileId);
            
            IEnumerable<Kweet> entities = await _context.Kweets
                .Where(x => x.Profile == profile)
                .OrderBy(x => x.DateOfCreation)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (entities == null)
            {
                return response;
            }

            response.Data = _mapper.Map<IEnumerable<Kweet>, IEnumerable<KweetDto>>(entities);
            response.Success = true;

            return response;
        }
    }
}