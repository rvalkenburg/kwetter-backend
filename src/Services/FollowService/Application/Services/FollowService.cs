using System;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.Common.Models;
using Kwetter.Services.FollowService.Application.Events;
using Kwetter.Services.FollowService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.FollowService.Application.Services
{
    public class FollowService : IFollowService
    {
        private readonly IFollowContext _context;
        private readonly IMapper _mapper;
        private readonly IProducer _producer;

        public FollowService(IFollowContext context, IMapper mapper, IProducer producer)
        {
            _context = context;
            _mapper = mapper;
            _producer = producer;
        }

        public async Task<Response<FollowDto>> CreateFollow(Guid profileId, Guid followerId)
        {
            var response = new Response<FollowDto>();

            var profile = await _context.Profile.FindAsync(profileId);
            var follower = await _context.Profile.FindAsync(followerId);

            var followConnectionExist = await _context.Follows.FirstOrDefaultAsync(x =>
                x.Profile == profile && x.Follower == follower);

            if (followConnectionExist != null) return response;

            var follow = new Follow
            {
                Id = new Guid(),
                Profile = profile,
                Follower = follower,
                DateOfCreation = DateTime.Now
            };


            _context.Follows.Add(follow);
            var success = await _context.SaveChangesAsync() > 0;
            if (success)
            {
                SendNewFollowEvent(follow.Profile.Id, follow.Follower.Id);
                response.Data = _mapper.Map<FollowDto>(follow);
                response.Success = true;
            }

            return response;
        }

        public async Task<Response<FollowDto>> DeleteFollow(Guid profileId, Guid followerId)
        {
            var response = new Response<FollowDto>();

            var follow =
                await _context.Follows.FirstOrDefaultAsync(
                    x => x.Follower.Id == followerId && x.Profile.Id == profileId);

            if (follow == null) return response;

            _context.Follows.Remove(follow);
            var success = await _context.SaveChangesAsync() > 0;

            if (success)
            {
                SendDeleteFollowEvent(profileId, followerId);
                response.Success = true;
            }

            return response;
        }

        private void SendNewFollowEvent(Guid profileId, Guid followerId)
        {
            var followEvent = new FollowEvent
            {
                ProfileId = profileId,
                FollowerId = followerId
            };

            var createFollowEvent = new Event<FollowEvent>
            {
                Data = followEvent
            };
            _producer.Send("Create-Follow", createFollowEvent);
        }

        private void SendDeleteFollowEvent(Guid profileId, Guid followerId)
        {
            var followEvent = new FollowEvent
            {
                ProfileId = profileId,
                FollowerId = followerId
            };

            var createFollowEvent = new Event<FollowEvent>
            {
                Data = followEvent
            };
            _producer.Send("Delete-Follow", createFollowEvent);
        }
    }
}