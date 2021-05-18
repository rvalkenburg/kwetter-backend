using System.Threading.Tasks;
using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Application.Events;
using Newtonsoft.Json;

namespace Kwetter.Services.SearchService.Application.EventHandlers.Profile
{
    public class UpdateProfileHandler : IUpdateProfileHandler
    {
        private readonly ISearchContext _context;

        public UpdateProfileHandler(ISearchContext context)
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