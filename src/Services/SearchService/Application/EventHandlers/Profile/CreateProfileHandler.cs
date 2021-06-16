using System.Collections.Generic;
using System.Threading.Tasks;
using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Application.Events;
using Newtonsoft.Json;

namespace Kwetter.Services.SearchService.Application.EventHandlers.Profile
{
    public class CreateProfileHandler : ICreateProfileHandler
    {
        private readonly ISearchContext _context;

        public CreateProfileHandler(ISearchContext context)
        {
            _context = context;
        }

        public async Task<bool> Consume(string message)
        {
            var profileEvent = JsonConvert.DeserializeObject<ProfileEvent>(message);

            if (profileEvent == null) return false;

            var profile = await _context.Profiles.FindAsync(profileEvent.Id);

            if (profile == null)
            {
                var newProfile = new Domain.Entities.Profile
                {
                    Id = profileEvent.Id,
                    DisplayName = profileEvent.DisplayName,
                    Avatar = profileEvent.Avatar,
                    Followers = new List<Domain.Entities.Follow>()
                };

                _context.Profiles.Add(newProfile);
                return await _context.SaveChangesAsync() > 0;
            }

            return false;
        }
    }
}