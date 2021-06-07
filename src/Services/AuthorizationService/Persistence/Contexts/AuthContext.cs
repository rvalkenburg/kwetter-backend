using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.AuthorizationService.Persistence.Contexts
{
    public class AuthContext : DbContext, IAuthContext
    {
        public AuthContext(DbContextOptions<AuthContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }
    }
}