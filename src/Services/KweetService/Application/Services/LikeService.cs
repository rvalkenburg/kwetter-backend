using System;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Services;
using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Domain.Entities;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class LikeService : ILikeService
    {
        private readonly IKweetContext _context;

        public LikeService(IKweetContext context)
        {
            _context = context;
        }

        public async Task<Response<int>> CreateLikeAsync(Guid profileId, Guid kweetId)
        {
            var response = new Response<int>();

            var kweet = await _context.Kweets.FindAsync(kweetId);
            var profile = await _context.Profiles.FindAsync(profileId);

            var like = new Like
            {
                Id = Guid.NewGuid(),
                ProfileId = profile.Id,
                KweetId = kweet.Id,
                DateOfCreation = DateTime.Now
            };

            await _context.Likes.AddAsync(like);
            var success = await _context.SaveChangesAsync() > 0;

            if (success)
            {
                response.Data = kweet.Likes.Count();
                response.Success = true;
            }

            return response;
        }

        public async Task<Response<int>> DeleteLikeAsync(Guid profileId, Guid kweetId)
        {
            var response = new Response<int>();

            var kweet = await _context.Kweets.FindAsync(kweetId);
            var profile = await _context.Profiles.FindAsync(profileId);

            var like = _context.Likes.FirstOrDefault(x => x.ProfileId == profile.Id && x.KweetId == kweet.Id);
            if (like != null)
            {
                _context.Likes.Remove(like);
                var success = await _context.SaveChangesAsync() > 0;
                if (success) response.Success = true;
            }

            return response;
        }
    }
}