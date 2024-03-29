﻿using System.Threading.Tasks;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.ProfileService.Persistence.Contexts
{
    public class ProfileContext : DbContext, IProfileContext 
    {
        public DbSet<Profile> Profiles { get; set; }
        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        public ProfileContext(DbContextOptions<ProfileContext> options) : base(options)
        {
        }
    }
}