using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Events;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces.Handlers
{
    public interface IHandler
    {
        Task<bool> Consume(string message);
    }
}