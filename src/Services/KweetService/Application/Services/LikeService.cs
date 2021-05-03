using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Domain.Entities;
using Profile = Kwetter.Services.KweetService.Domain.Entities.Profile;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly IKweetContext _context;
        private readonly IMapper _mapper;
        
        public LikeService(IKweetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        public async Task<Response<int>> CreateLikeAsync(Guid profileId, Guid kweetId)
        {
            Response<int> response = new Response<int>();
            
            Kweet kweet = await _context.Kweets.FindAsync(kweetId);
            Profile profile = await _context.Profiles.FindAsync(profileId);

            Like like = new Like
            {
                Id = new Guid(),
                Profile = profile,
                Kweet = kweet,
                DateOfCreation = DateTime.Now,
            };

            await _context.Likes.AddAsync(like);
            bool success = await _context.SaveChangesAsync() > 0;

            if (success)
            {
                response.Data = kweet.Likes.Count();
                response.Success = true;
            }

            return response;
        }

        public async Task<Response<int>> DeleteLikeAsync(Guid id)
        {
            Response<int> response = new Response<int>();

            Like like = new Like()
            {
                Id = id,
            };
            
            _context.Likes.Remove(like);
            bool success = await _context.SaveChangesAsync() > 0;

            if (success)
            {
                response.Success = true;
            }

            return response;
        }
    }
}