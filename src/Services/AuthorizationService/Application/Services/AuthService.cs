using System.Collections.Generic;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;

namespace Kwetter.Services.AuthorizationService.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly FirebaseAuth _firebaseApp;
        public AuthService(FirebaseApp firebaseApp)
        {
            _firebaseApp = FirebaseAuth.GetAuth(firebaseApp);
        }
        public async Task<bool> SetUserClaims(string uid)
        {
            var token = await _firebaseApp.VerifyIdTokenAsync(uid);
            var claims = new Dictionary<string, object>()
            {
                { "user", true },
            };
            await _firebaseApp.SetCustomUserClaimsAsync(token.Subject, claims);
            return true;
        }
    }
}