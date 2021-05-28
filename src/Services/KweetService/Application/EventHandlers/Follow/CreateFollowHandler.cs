using System;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.KweetService.Application.Events;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Kwetter.Services.KweetService.Application.EventHandlers.Follow
{
    public class CreateFollowHandler : ICreateFollowHandler
    {
        private readonly IKweetContext _context;

        public CreateFollowHandler(IKweetContext context)
        {
            _context = context;
        }
        public async Task<bool> Consume(string message)
        {
            FollowEvent followEvent = JsonConvert.DeserializeObject<FollowEvent>(message);
            Domain.Entities.Profile profile = await _context.Profiles.FindAsync(followEvent.ProfileId);
            Domain.Entities.Profile follower = await _context.Profiles.FindAsync(followEvent.FollowerId);
            
            Domain.Entities.Follow followConnectionExist = await _context.Follows.FirstOrDefaultAsync(x =>
                x.Profile == profile && x.Follower == follower);
            
            if (followConnectionExist == null)
            {
                Domain.Entities.Follow newFollow = new Domain.Entities.Follow
                {
                    Id = new Guid(),
                    Profile = profile,
                    Follower = follower
                };
                _context.Follows.Add(newFollow);
                return await _context.SaveChangesAsync() > 0;
            }
            
            return false;
        }
        
    }
}