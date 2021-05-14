using System.Threading.Tasks;
using Kwetter.Services.ProfileService.Application.Common.Models;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces
{
    public interface IProducer
    {
        Task<bool> Send<T>(string topic, Event<T> @event);
    }
}