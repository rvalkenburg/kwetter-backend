using System.Collections.Generic;
using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Application.Common.Models;

namespace Kwetter.Services.AuthorizationService.Application.Common.Interfaces
{
    public interface ITokenVerifier
    {
        Task<ClaimsDto> VerifyTokenAsync(string jwt);
        Task<bool> AddClaims(string jwt, Dictionary<string, object> claims);
    }
}