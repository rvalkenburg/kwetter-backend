using System.Threading.Tasks;
using Kwetter.Services.AuthService.Application.Common.Models;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces
{
    public interface IAuthHttpRequest
    {
        Task<Response<AuthResponseDto>> SendAuthRequest(string code);
    }
}