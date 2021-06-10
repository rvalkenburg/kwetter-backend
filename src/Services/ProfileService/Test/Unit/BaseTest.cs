using System;
using Kwetter.Services.ProfileService.Domain.Entity;
using Kwetter.Services.ProfileService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.ProfileService.Test.Unit
{
    public abstract class BaseTest
    {
        protected ProfileContext Context { get; private set; }
        protected Profile TestProfile1 { get; private set; }
        protected Profile TestProfile2 { get; private set; }

        protected void Init()
        {
            var dbContext = new DbContextOptionsBuilder<ProfileContext>().UseInMemoryDatabase("Test");
            Context = new ProfileContext(dbContext.Options);
            Context.Database.EnsureCreated();
        }
        
        private void SetProfiles()
        {
            TestProfile1 = new Profile {Id = new Guid(), DisplayName = "Roger", Avatar = "Test.png", GoogleId = "00000000"};
            TestProfile2 = new Profile {Id = new Guid(), DisplayName = "John", Avatar = "Test.png", GoogleId = "11111111"};
            Context.Add(TestProfile1);
            Context.Add(TestProfile2);
            Context.SaveChangesAsync();
        }
    }
}