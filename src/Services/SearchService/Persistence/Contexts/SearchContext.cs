using System.Threading.Tasks;
using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.SearchService.Persistence.Contexts
{
    public class SearchContext : DbContext, ISearchContext
    {
        public DbSet<Profile> Profiles { get; set; }
        
        public DbSet<Follow> Follow { get; set; }


        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public SearchContext(DbContextOptions<SearchContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .HasMany(c => c.Followers)
                .WithOne(e => e.Profile);

            modelBuilder.Entity<Follow>()
                .HasOne(c => c.Follower);


        }        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
    }
}