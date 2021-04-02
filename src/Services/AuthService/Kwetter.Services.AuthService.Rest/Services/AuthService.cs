using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kwetter.Services.AuthService.Rest.Interfaces;
using Kwetter.Services.AuthService.Rest.Models.Entity;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Kwetter.Services.AuthService.Rest.Services
{
    public class AuthService : IAuthService
    {
                private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtConfig _jwtConfig;

        public AuthService(UserManager<IdentityUser> userManager, JwtConfig jwtConfig)
        {
            _userManager = userManager;
            _jwtConfig = jwtConfig;
        }
        public async Task<AuthenticationResult> RegisterAsync(string email, string password)
        {
            IdentityUser existingUser = await _userManager.FindByEmailAsync(email);
            if (existingUser != null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this email address already exists"}
                };
            }

            IdentityUser newUser = new IdentityUser
            {
                Email = email,
                UserName = email,
            };

            IdentityResult createdUser = await _userManager.CreateAsync(newUser, password);

            if (!createdUser.Succeeded)
            {
                return new AuthenticationResult
                {
                    Errors = createdUser.Errors.Select(x => x.Description)
                };
            }
            return CreateToken(newUser);
        }

        public async Task<AuthenticationResult> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User with this email address already exists"}
                };
            }

            bool passwordMatches = await _userManager.CheckPasswordAsync(user, password);

            if (!passwordMatches)
            {
                return new AuthenticationResult
                {
                    Errors = new[] {"User/password does not match"}
                };
            }

            return CreateToken(user);
        }
        
        private AuthenticationResult CreateToken(IdentityUser user)
        {
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret); 
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                    new Claim(JwtRegisteredClaimNames.Email, user.Email),
                    new Claim("id", user.Id)

                }),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return new AuthenticationResult
            {
                Token = tokenHandler.WriteToken(token),
                Success = true
            };
        }
    }
}