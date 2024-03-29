﻿using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.KweetService.Persistence.Contexts
{
    public class KweetContext : DbContext, IKweetContext
    {
        public DbSet<Kweet> Kweets { get; set; }
        public DbSet<Like> Likes { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Follow> Follows { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public KweetContext(DbContextOptions<KweetContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Profile>()
                .HasMany(x => x.Follows)
                .WithOne(x => x.Follower);
            base.OnModelCreating(modelBuilder);
        }
    }
}