using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.KweetService.Application.Events;
using Newtonsoft.Json;

namespace Kwetter.Services.KweetService.Application.EventHandlers.Profile
{
    public class UpdateProfileHandler : IUpdateProfileHandler
    {
        private readonly IKweetContext _context;

        public UpdateProfileHandler(IKweetContext context)
        {
            _context = context;
        }
        public async Task<bool> Consume(string message)
        {
            ProfileEvent profileEvent =  JsonConvert.DeserializeObject<ProfileEvent>(message);
            if (profileEvent == null) return false;
            
            Domain.Entities.Profile profile = await _context.Profiles.FindAsync(profileEvent.Id);

            if (profile != null)
            {
                profile.Avatar = profileEvent.Avatar;
                profile.DisplayName = profileEvent.DisplayName;
                _context.Profiles.Update(profile);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}