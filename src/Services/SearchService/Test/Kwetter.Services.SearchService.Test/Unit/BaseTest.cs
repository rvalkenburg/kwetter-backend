using System;
using Kwetter.Services.SearchService.Domain.Entities;
using Kwetter.Services.SearchService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.SearchService.Test.Unit
{
    public abstract class BaseTest
    {
        protected SearchContext Context { get; private set; }
        protected Profile TestProfile1 { get; private set; }
        protected Profile TestProfile2 { get; private set; }

        protected void Init()
        {
            var dbContext = new DbContextOptionsBuilder<SearchContext>().UseInMemoryDatabase("Test");
            Context = new SearchContext(dbContext.Options);
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