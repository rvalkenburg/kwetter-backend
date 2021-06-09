using System;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.EventHandlers.Profile;
using Kwetter.Services.KweetService.Application.Events;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Kwetter.Services.KweetService.Test.Unit.EventHandlers
{
    public class UpdateProfileHandlerTest : BaseTest
    {
        private UpdateProfileHandler _updateProfileHandler;

        [SetUp]
        public void Setup()
        {
            Init();
            _updateProfileHandler = new UpdateProfileHandler(Context);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        [Test]
        public async Task UpdateProfileWhileProfileExists()
        {
            var profile = TestProfile1;
            profile.DisplayName = "John Doe";

            var profileEvent = new ProfileEvent
                {Id = TestProfile1.Id, Avatar = TestProfile1.Avatar, DisplayName = "John Doe"};

            var success = await _updateProfileHandler.Consume(JsonConvert.SerializeObject(profileEvent));

            Assert.True(success);
            Assert.AreEqual("John Doe", TestProfile1.DisplayName);
        }

        [Test]
        public async Task UpdateProfileWhileProfileDoesNotExists()
        {
            var profileEvent = new ProfileEvent
            {
                Id = new Guid(),
                DisplayName = "Roger",
                Avatar = "Test.png"
            };
            var success = await _updateProfileHandler.Consume(JsonConvert.SerializeObject(profileEvent));
            Assert.False(success);
        }
    }
}