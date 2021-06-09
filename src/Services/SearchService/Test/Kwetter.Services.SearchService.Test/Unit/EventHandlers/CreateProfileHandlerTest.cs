using System;
using System.Threading.Tasks;
using Kwetter.Services.SearchService.Application.EventHandlers.Profile;
using Kwetter.Services.SearchService.Application.Events;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Kwetter.Services.SearchService.Test.Unit.EventHandlers
{
    public class CreateProfileHandlerTest : BaseTest
    {
        private CreateProfileHandler _createProfileHandler;

        [SetUp]
        public void Setup()
        {
            Init();
            _createProfileHandler = new CreateProfileHandler(Context);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        [Test]
        public async Task CreateProfileWhileProfileExists()
        {
            var profileEvent = new ProfileEvent
            {
                Id = TestProfile1.Id,
                DisplayName = TestProfile1.DisplayName,
                Avatar = TestProfile1.Avatar
            };

            var success = await _createProfileHandler.Consume(JsonConvert.SerializeObject(profileEvent));

            Assert.False(success);
        }

        [Test]
        public async Task CreateProfileWhileProfileDoesNotExist()
        {
            var profileEvent = new ProfileEvent
            {
                Id = new Guid(),
                DisplayName = "Test",
                Avatar = "Test.png"
            };

            var success = await _createProfileHandler.Consume(JsonConvert.SerializeObject(profileEvent));

            Assert.True(success);
        }
    }
}