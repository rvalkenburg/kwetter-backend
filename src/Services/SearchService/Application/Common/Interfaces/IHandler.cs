using System.Threading.Tasks;

namespace Kwetter.Services.SearchService.Application.Common.Interfaces
{
    public interface IHandler
    {
        Task<bool> Consume(string message);
    }
}