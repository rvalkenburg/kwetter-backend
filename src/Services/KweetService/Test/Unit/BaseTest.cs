using System;
using Kwetter.Services.KweetService.Domain.Entities;
using Kwetter.Services.KweetService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.KweetService.Test.Unit
{
    public abstract class BaseTest
    {
        protected KweetContext Context { get; private set; }
        protected Profile TestProfile1 { get; private set; }
        protected Profile TestProfile2 { get; private set; }

        protected void Init()
        {
            var dbContext = new DbContextOptionsBuilder<KweetContext>().UseInMemoryDatabase("Test");
            Context = new KweetContext(dbContext.Options);
            Context.Database.EnsureCreated();
            SetProfiles();
        }

        private void SetProfiles()
        {
            TestProfile1 = new Profile {Id = new Guid(), DisplayName = "Roger", Avatar = "Test.png"};
            TestProfile2 = new Profile {Id = new Guid(), DisplayName = "John", Avatar = "Test.png"};
            Context.Add(TestProfile1);
            Context.Add(TestProfile2);
            Context.SaveChangesAsync();
        }
    }
}