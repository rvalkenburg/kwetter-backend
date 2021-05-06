using System;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.Common.Models;
using Kwetter.Services.FollowService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Profile = Kwetter.Services.FollowService.Domain.Entities.Profile;

namespace Kwetter.Services.FollowService.Application.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowContext _context;
        private readonly IMapper _mapper;

        public FollowService(IFollowContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<Response<FollowDto>> CreateFollow(Guid profileId, Guid followerId)
        {
            Response<FollowDto> response = new Response<FollowDto>();

            Profile profile = await _context.Profile.FindAsync(profileId);
            Profile follower = await _context.Profile.FindAsync(followerId);

            Follow followConnectionExist = await _context.Follows.FirstOrDefaultAsync(x =>
                x.Profile == profile && x.Follower == follower);

            if (followConnectionExist != null) return response;

            Follow follow = new Follow
            {
                Id = new Guid(),
                Profile = profile,
                Follower = follower,
                DateOfCreation = DateTime.Now
            };

            _context.Follows.Add(follow);
            bool success = await _context.SaveChangesAsync() > 0;
            if (success)
            {
                response.Data = _mapper.Map<FollowDto>(follow);
                response.Success = true;
            }

            return response;
        }

        public async Task<Response<FollowDto>> DeleteFollow(Guid id)
        {
            Response<FollowDto> response = new Response<FollowDto>();

            Follow follow = await _context.Follows.FindAsync(id);

            if (follow == null) return response;

            _context.Follows.Remove(follow);
            bool success = await _context.SaveChangesAsync() > 0;

            if (success)
            {
                response.Success = true;
            }

            return response;
        }
    }
}