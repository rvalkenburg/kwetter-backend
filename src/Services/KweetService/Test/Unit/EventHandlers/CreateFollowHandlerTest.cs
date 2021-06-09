using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.EventHandlers.Follow;
using Kwetter.Services.KweetService.Application.Events;
using Kwetter.Services.KweetService.Domain.Entities;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Kwetter.Services.KweetService.Test.Unit.EventHandlers
{
    public class CreateFollowHandlerTest : BaseTest
    {
        private CreateFollowHandler _createFollowHandler;

        [SetUp]
        public void Setup()
        {
            Init();
            _createFollowHandler = new CreateFollowHandler(Context);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        [Test]
        public async Task CreateFollowWhileFollowExists()
        {
            var follow = new Follow {Profile = TestProfile1, Follower = TestProfile2};
            var followEvent = new FollowEvent {FollowerId = follow.Follower.Id, ProfileId = follow.Profile.Id};

            Context.Follows.Add(follow);
            await Context.SaveChangesAsync();
            var success = await _createFollowHandler.Consume(JsonConvert.SerializeObject(followEvent));

            Assert.False(success);
        }

        [Test]
        public async Task CreateFollowWhileFollowDoesNotExist()
        {
            var followEvent = new FollowEvent {ProfileId = TestProfile1.Id, FollowerId = TestProfile2.Id};

            var success = await _createFollowHandler.Consume(JsonConvert.SerializeObject(followEvent));

            Assert.True(success);
        }
    }
}