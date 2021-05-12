using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;

namespace Kwetter.Services.KweetService.Application.EventHandlers
{
    public class UpdateProfileHandler : IProfileHandler
    {
        private readonly IProfileService _service;
        
        public UpdateProfileHandler(IProfileService service)
        {
            _service = service;
        }
        public async Task<bool> Consume(string message)
        {
            //return await _service.AddOrUpdateProfile(@event);
            return true;
        }
    }
}