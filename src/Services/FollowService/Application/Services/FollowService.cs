using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly INewFollowEvent _newFollowEvent;
        private readonly IDeleteFollowEvent _deleteFollowEvent;

        public FollowService(IFollowContext context, IMapper mapper, INewFollowEvent newFollowEvent, IDeleteFollowEvent deleteFollowEvent)
        {
            _context = context;
            _mapper = mapper;
            _deleteFollowEvent = deleteFollowEvent;
            _newFollowEvent = newFollowEvent;
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
            _newFollowEvent.SendNewFollowEvent(new FollowEvent
                {ProfileId = follow.Profile.Id, FollowerId = follow.Follower.Id});

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

        public async Task<Response<IEnumerable<FollowDto>>> GetPaginatedFollowersByProfileId(Guid id)
        {
            Response<IEnumerable<FollowDto>> response = new Response<IEnumerable<FollowDto>>();

            Profile profile = await _context.Profile.FindAsync(id);
            
            if (profile == null) return response;

            List<Follow> follows = await _context.Follows.Where(x => x.Profile == profile).ToListAsync();

            if (follows != null)
            {
                response.Data = _mapper.Map<IEnumerable<FollowDto>>(follows);
                response.Success = true;
            }
            return response;
        }
        
        public async Task<Response<IEnumerable<FollowingDto>>> GetPaginatedFollowingByProfileId(Guid id)
        {
            Response<IEnumerable<FollowingDto>> response = new Response<IEnumerable<FollowingDto>>();

            Profile profile = await _context.Profile.FindAsync(id);
            
            if (profile == null) return response;

            List<Follow> follows = await _context.Follows.Where(x => x.Follower == profile).ToListAsync();

            if (follows != null)
            {
                response.Data = _mapper.Map<IEnumerable<FollowingDto>>(follows);
                response.Success = true;
            }
            return response;
        }
    }
}