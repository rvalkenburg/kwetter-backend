using System.Threading.Tasks;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces.Handlers
{
    public interface IHandler
    {
        Task<bool> Consume(string message);
    }
}