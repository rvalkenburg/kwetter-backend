using System.Threading.Tasks;

namespace Kwetter.Services.AuthorizationService.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<bool> SetUserClaims(string uid);
    }
}