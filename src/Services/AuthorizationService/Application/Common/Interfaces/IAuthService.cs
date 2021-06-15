using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Application.Common.Models;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.AuthorizationService.Application.Common.Interfaces
{
    public interface IAuthService
    {
        Task<Response<UserDto>> SetUserClaims(string uid, ILogger logger);
    }
}