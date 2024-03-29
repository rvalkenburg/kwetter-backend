﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Application.Events;
using Newtonsoft.Json;

namespace Kwetter.Services.SearchService.Application.EventHandlers.Follow
{
    public class CreateFollowHandler : ICreateFollowHandler
    {
        private readonly ISearchContext _context;

        public CreateFollowHandler(ISearchContext context)
        {
            _context = context;
        }

        public async Task<bool> Consume(string message)
        {
            var followEvent = JsonConvert.DeserializeObject<FollowEvent>(message);
            var profile = await _context.Profiles.FindAsync(followEvent.ProfileId);
            var follower = await _context.Profiles.FindAsync(followEvent.FollowerId);

            if (profile != null && follower != null)
            {
                profile.Followers ??= new List<Domain.Entities.Follow>();

                var followConnectionExist = profile.Followers.Any(x => x.Follower.Id == follower.Id);

                if (!followConnectionExist)
                {
                    var follow = new Domain.Entities.Follow
                    {
                        Id = Guid.NewGuid(),
                        DateOfCreation = DateTime.Now,
                        Follower = follower,
                        Profile = profile
                    };
                    profile.Followers.Add(follow);
                    _context.Follow.Add(follow);
                    _context.Profiles.Update(profile);
                    return await _context.SaveChangesAsync() > 0;
                }
            }

            return false;
        }
    }
}