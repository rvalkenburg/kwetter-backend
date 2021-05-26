using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using FirebaseAdmin;
using FirebaseAdmin.Auth;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Application.Common.Models;

namespace Kwetter.Services.AuthorizationService.Infrastructure.Authorization
{
    public class FirebaseTokenVerifier : ITokenVerifier
    {
        private readonly FirebaseAuth _firebaseApp;

        public FirebaseTokenVerifier(FirebaseApp firebaseApp)
        {
            _firebaseApp = _firebaseApp = FirebaseAuth.GetAuth(firebaseApp);;
        }
        
        public async Task<ClaimsDto> VerifyTokenAsync(string jwt)
        {
            try
            {
                FirebaseToken token = await _firebaseApp.VerifyIdTokenAsync(jwt);
            
                ClaimsDto claimsDto = new ClaimsDto
                {
                    Subject = token.Subject,
                    Audience = token.Audience,
                    Issuer = token.Issuer,
                    ExpirationTimeSeconds = token.ExpirationTimeSeconds,
                    IssuedAtTimeSeconds = token.IssuedAtTimeSeconds,
                    Claims = token.Claims,
                };
                
                return claimsDto;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        public async Task<bool> AddClaims(string jwt, Dictionary<string, object> claims)
        {
            await _firebaseApp.SetCustomUserClaimsAsync(jwt, claims);
            return true;
        }
    }
}