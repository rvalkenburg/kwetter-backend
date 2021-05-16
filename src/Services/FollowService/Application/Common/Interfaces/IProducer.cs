using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Events;

namespace Kwetter.Services.FollowService.Application.Common.Interfaces
{
    public interface IProducer
    {
        Task<bool> Send<T>(string topic, Event<T> @event);
    }
}