using System.Threading.Tasks;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Application.Events;
using Newtonsoft.Json;

namespace Kwetter.Services.KweetService.Application.EventHandlers
{
    public class NewFollowHandler : IFollowHandler
    {
        private readonly IFollowService _service;

        public NewFollowHandler(IFollowService service)
        {
            _service = service;
        }
        public async Task<bool> Consume(string message)
        {
            //return await _service.AddOrDeleteFollow(@event.ProfileId, @event.FollowerId);
            return true;
        }
    }
}