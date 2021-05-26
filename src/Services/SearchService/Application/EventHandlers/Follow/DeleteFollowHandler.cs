using System.Linq;
using System.Threading.Tasks;
using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Application.Events;
using Newtonsoft.Json;

namespace Kwetter.Services.SearchService.Application.EventHandlers.Follow
{
    public class DeleteFollowHandler : IDeleteFollowHandler
    {        
        private readonly ISearchContext _context;

        public DeleteFollowHandler(ISearchContext context)
        {
            _context = context;
        }
        
        public async Task<bool> Consume(string message)
        {
            FollowEvent followEvent = JsonConvert.DeserializeObject<FollowEvent>(message);
            
            if (followEvent == null) return false;
            
            Domain.Entities.Follow followConnectionExist = _context.Follow.FirstOrDefault(x => x.Profile.Id == followEvent.ProfileId && x.Follower.Id == followEvent.FollowerId);
            
            if (followConnectionExist != null)
            {
                _context.Follow.Remove(followConnectionExist);
                return await _context.SaveChangesAsync() > 0;
            }
            return true;
        }
    }
}