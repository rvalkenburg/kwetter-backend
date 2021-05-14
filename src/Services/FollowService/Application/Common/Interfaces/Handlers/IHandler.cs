using System.Threading.Tasks;

namespace Kwetter.Services.FollowService.Application.Common.Interfaces.Handlers
{
    public interface IHandler
    {
        Task<bool> Consume(string message);
    }
}