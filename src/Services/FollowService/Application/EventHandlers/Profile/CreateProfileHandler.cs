using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.FollowService.Application.Events;
using Newtonsoft.Json;

namespace Kwetter.Services.FollowService.Application.EventHandlers.Profile
{
    public class CreateProfileHandler : ICreateProfileHandler
    {
        private readonly IFollowContext _context;
        
        public CreateProfileHandler(IFollowContext context)
        {
            _context = context;
        }
                
        public async Task<bool> Consume(string message)
        {
            ProfileEvent profileEvent =  JsonConvert.DeserializeObject<ProfileEvent>(message);
            if (profileEvent == null) return false;
            
            Domain.Entities.Profile profile = await _context.Profile.FindAsync(profileEvent.Id);

            if (profile == null)
            {
                Domain.Entities.Profile newProfile = new Domain.Entities.Profile
                {
                    Id = profileEvent.Id,
                    DisplayName = profileEvent.DisplayName,
                    Avatar = profileEvent.Avatar
                };
            
                _context.Profile.Add(newProfile);
                return await _context.SaveChangesAsync() > 0;
            }
            return false;
        }
    }
}