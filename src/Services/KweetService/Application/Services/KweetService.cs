using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Services;
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
            bool addedTags = await GetHashTagsFromMessage(kweet);
            if (success && addedTags)
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

        public async Task<Response<IEnumerable<KweetDto>>> GetPaginatedTimeline(int pageNumber, int pageSize, Guid profileId)
        {
            Response<IEnumerable<KweetDto>> response = new();
            
            Profile profile = await _context.Profiles.FindAsync(profileId);

            List<Follow> follows = await _context.Follows.Where(x => x.Profile == profile).ToListAsync();

            List<Kweet> kweets = await _context.Kweets.Where(x => follows.Select(y => y.Follower)
                .Contains(x.Profile))
                .OrderBy(x => x.DateOfCreation)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (kweets != null)
            {
                response.Data = _mapper.Map<IEnumerable<Kweet>, IEnumerable<KweetDto>>(kweets);;
            }

            response.Success = true;

            return response;
        }

        public async Task<Response<IEnumerable<KweetDto>>> GetPaginatedTrendingKweets(int pageNumber, int pageSize)
        {
            Response<IEnumerable<KweetDto>> response = new();

            return response;

        }

        private async Task<bool> GetHashTagsFromMessage(Kweet kweet)
        {
            List<HashTag> tags = new List<HashTag>();
            
            var regex = new Regex(@"#\w+");
            var matches = regex.Matches(kweet.Message);
            if (matches.Count > 0)
            {
                foreach (var match in matches)
                {
                    HashTag tag = new HashTag
                    {
                        Tag = match.ToString().ToLower(),
                        Kweet = kweet,
                    };
                    tags.Add(tag);
                }
                _context.Tags.AddRange(tags);
                bool success = await _context.SaveChangesAsync() > 0;
                if (!success)
                {
                    return false;
                }
            }
            return true;
        }
    }
}