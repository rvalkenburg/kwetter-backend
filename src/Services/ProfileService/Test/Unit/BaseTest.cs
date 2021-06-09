using Kwetter.Services.ProfileService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.ProfileService.Test.Unit
{
    public abstract class BaseTest
    {
        protected ProfileContext Context { get; private set; }

        protected void Init()
        {
            var dbContext = new DbContextOptionsBuilder<ProfileContext>().UseInMemoryDatabase("Test");
            Context = new ProfileContext(dbContext.Options);
            Context.Database.EnsureCreated();
        }
    }
}