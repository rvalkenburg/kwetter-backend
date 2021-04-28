using System.Threading.Tasks;
using Kwetter.Services.AuthService.Application.Common.Interfaces;
using Kwetter.Services.AuthService.Domain;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.AuthService.Persistence.Contexts
{
    public class AuthContext : DbContext, IAuthContext 
    {
        public DbSet<User> Users { get; set; }
    
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }
    }
}