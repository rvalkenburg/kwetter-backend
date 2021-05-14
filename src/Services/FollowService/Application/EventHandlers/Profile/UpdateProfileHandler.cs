using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.FollowService.Application.Events;
using Newtonsoft.Json;

namespace Kwetter.Services.FollowService.Application.EventHandlers.Profile
{
    public class UpdateProfileHandler : IUpdateProfileHandler
    {
        private readonly IFollowContext _context;

        public UpdateProfileHandler(IFollowContext context)
        {
            _context = context;
        }
        public async Task<bool> Consume(string message)
        {
            ProfileEvent profileEvent =  JsonConvert.DeserializeObject<ProfileEvent>(message);
            if (profileEvent == null) return false;
            
            Domain.Entities.Profile profile = await _context.Profile.FindAsync(profileEvent.Id);

            if (profile != null)
            {
                profile.Avatar = profileEvent.Avatar;
                profile.DisplayName = profileEvent.DisplayName;
                _context.Profile.Update(profile);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}