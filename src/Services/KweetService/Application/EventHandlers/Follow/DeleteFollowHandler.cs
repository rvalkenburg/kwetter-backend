using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.KweetService.Application.Events;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Kwetter.Services.KweetService.Application.EventHandlers.Follow
{
    public class DeleteFollowHandler : IDeleteFollowHandler
    {
        private readonly IKweetContext _context;

        public DeleteFollowHandler(IKweetContext context)
        {
            _context = context;
        }
        
        public async Task<bool> Consume(string message)
        {
            FollowEvent followEvent = JsonConvert.DeserializeObject<FollowEvent>(message);
            Domain.Entities.Profile profile = await _context.Profiles.FindAsync(followEvent.ProfileId);
            Domain.Entities.Profile follower = await _context.Profiles.FindAsync(followEvent.FollowerId);
            
            Domain.Entities.Follow follow = await _context.Follows.FirstOrDefaultAsync(x =>
                x.Profile == profile && x.Follower == follower);
            
            if (follow != null)
            {
                _context.Follows.Remove(follow);
                return await _context.SaveChangesAsync() > 0;
            }
            
            return false;
        }
    }
}