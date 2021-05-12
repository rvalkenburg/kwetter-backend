using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Events;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IHandler
    {
        Task<bool> Consume(string message);
    }
}