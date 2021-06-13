using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.FollowService.Persistence.Contexts
{
    public class FollowContext : DbContext, IFollowContext
    {
        public FollowContext(DbContextOptions<FollowContext> options) : base(options)
        {
        }

        public DbSet<Follow> Follows { get; set; }
        public DbSet<Profile> Profile { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}