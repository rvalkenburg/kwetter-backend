using System.Threading.Tasks;
using Kwetter.Services.AuthService.Rest.Models.Entity;

namespace Kwetter.Services.AuthService.Rest.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        Task<AuthenticationResult> LoginAsync(string email, string password);
    }
}