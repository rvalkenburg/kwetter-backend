using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Application.Common.Models;
using Microsoft.EntityFrameworkCore;
using Profile = Kwetter.Services.SearchService.Domain.Entities.Profile;

namespace Kwetter.Services.SearchService.Application.Services
{
    public class SearchService : ISearchService
    {
        private readonly ISearchContext _context;
        private readonly IMapper _mapper;
        
        public SearchService(ISearchContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<PaginationResponse<SearchDto>> GetPaginatedSearch(int pageSize, int pageNumber, string name, Guid profileId)
        {
            
            PaginationResponse<SearchDto> response = new();

            Profile currentUser = await _context.Profiles.FindAsync(profileId);
            
            List<Profile> entities = await _context.Profiles
                .Where(x => x.DisplayName == name)
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();


                response.Data = _mapper.Map<IEnumerable<Profile>, IEnumerable<SearchDto>>(entities,
                    opt => opt.AfterMap((src, dest) =>
                    {
                        foreach (var i in dest)
                        {
                            i.Status = currentUser.Followers.Any(x => x.Follower.Id == i.Id);;
                        }
                    }));
            
            response.PageSize = pageSize;
            response.PageNumber = pageNumber;
            response.Success = true;

            return response;
        }
        
    }
}