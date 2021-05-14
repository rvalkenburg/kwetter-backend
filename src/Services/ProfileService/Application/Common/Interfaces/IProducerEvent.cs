using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces
{
    public interface IProducerEvent
    {
        Task<bool> Send(string message);
    }
}