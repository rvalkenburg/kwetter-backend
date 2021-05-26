using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Application.Common.Models;
using Kwetter.Services.AuthorizationService.Application.Events;

namespace Kwetter.Services.AuthorizationService.Application.Common.Interfaces
{
    public interface IProducer
    {
        Task<bool> Send<T>(string topic, Event<T> @event);
    }
}