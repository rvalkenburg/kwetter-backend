using System;
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
            FollowEvent followEvent = JsonConvert.DeserializeObject<FollowEvent>(message);
            Domain.Entities.Profile profile = await _context.Profiles.FindAsync(followEvent.ProfileId);
            Domain.Entities.Profile follower = await _context.Profiles.FindAsync(followEvent.FollowerId);

            var followConnectionExist = profile.Followers.FirstOrDefault(x => x.Id == follower.Id);

            if (followConnectionExist == null)
            {
                profile.Followers.Add(new Domain.Entities.Follow
                {
                    Id = new Guid(),
                    DateOfCreation = DateTime.Now,
                    Follower = follower
                });
                _context.Profiles.Update(profile);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}