using System;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class FollowService : IFollowService
    {
        private readonly IKweetContext _context;
        
        public FollowService(IKweetContext context)
        {
            _context = context;
        }
        
        public async Task<bool> AddOrDeleteFollow(Guid profileId, Guid followerId)
        {
            Profile profile = await _context.Profiles.FindAsync(profileId);
            Profile follower = await _context.Profiles.FindAsync(followerId);

            Follow followConnectionExist = await _context.Follows.FirstOrDefaultAsync(x =>
                x.Profile == profile && x.Follower == follower);

            if (followConnectionExist == null)
            {
                Follow follow = new Follow
                {
                    Id = new Guid(),
                    Profile = profile,
                    Follower = follower
                };
                return await AddFollow(follow);
            }
            return await DeleteFollow(followConnectionExist);
        }

        private async Task<bool> AddFollow(Follow follow)
        {
            _context.Follows.Add(follow);
            return await _context.SaveChangesAsync() > 0;
        }

        private async Task<bool> DeleteFollow(Follow follow)
        {
            _context.Follows.Remove(follow);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}