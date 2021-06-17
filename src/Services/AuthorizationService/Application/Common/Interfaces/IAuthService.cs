using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Application.Common.Models;

namespace Kwetter.Services.AuthorizationService.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<Response<UserDto>> SetUserClaims(string uid);
        Task<bool> SetAdminClaims(string uid);
    }
}