using System;
using System.Threading.Tasks;
using Kwetter.Services.FollowService.Application.EventHandlers.Profile;
using Kwetter.Services.FollowService.Domain.Entities;
using Kwetter.Services.FollowService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

namespace Kwetter.Services.FollowService.Test.Unit.EventHandlers
{
    public class UpdateProfileHandlerTest
    {
        private FollowContext _context;
        private UpdateProfileHandler _updateProfileHandler;

        [SetUp]
        public void Setup()
        {
            var dbContext = new DbContextOptionsBuilder<FollowContext>()
                .UseInMemoryDatabase("KwetterFollowDatabase")
                .Options;
            _context = new FollowContext(dbContext);

            _updateProfileHandler = new UpdateProfileHandler(_context);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task UpdateProfileWhileProfileDoesNotExist()
        {
            var message =
                "{\"Id\":\"7ad3918c-bbcb-4e8a-af79-4219940c9be0\",\"DisplayName\":\"Roger\",\"Avatar\":\"Test.png\"}";
            var success = await _updateProfileHandler.Consume(message);

            Assert.False(success);
        }

        [Test]
        public async Task UpdateProfileWhileProfileDoestExist()
        {
            var profile = new Profile
            {
                Id = Guid.Parse("7ad3918c-bbcb-4e8a-af79-4219940c9be0"),
                DisplayName = "Roger",
                Avatar = "Test.png"
            };
            _context.Profile.Add(profile);
            await _context.SaveChangesAsync();

            var message =
                "{\"Id\":\"7ad3918c-bbcb-4e8a-af79-4219940c9be0\",\"DisplayName\":\"Roger\",\"Avatar\":\"Test.png\"}";
            var success = await _updateProfileHandler.Consume(message);

            Assert.True(success);
        }
    }
}