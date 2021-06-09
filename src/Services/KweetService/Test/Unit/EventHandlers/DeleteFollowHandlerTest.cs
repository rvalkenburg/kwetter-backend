using System;
using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.EventHandlers.Follow;
using Kwetter.Services.KweetService.Application.Events;
using Kwetter.Services.KweetService.Domain.Entities;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Kwetter.Services.KweetService.Test.Unit.EventHandlers
{
    public class DeleteFollowHandlerTest : BaseTest
    {
        private DeleteFollowHandler _deleteFollowHandler;

        [SetUp]
        public void Setup()
        {
            Init();
            _deleteFollowHandler = new DeleteFollowHandler(Context);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        [Test]
        public async Task DeleteFollowWhileFollowExists()
        {
            var followEvent = new FollowEvent
            {
                ProfileId = TestProfile1.Id,
                FollowerId = TestProfile2.Id
            };

            Context.Follows.Add(new Follow {Id = new Guid(), Profile = TestProfile1, Follower = TestProfile2});
            await Context.SaveChangesAsync();
            var success = await _deleteFollowHandler.Consume(JsonConvert.SerializeObject(followEvent));

            Assert.True(success);
        }

        [Test]
        public async Task DeleteFollowWhileFollowDoesNotExists()
        {
            var followEvent = new FollowEvent
            {
                ProfileId = TestProfile1.Id,
                FollowerId = TestProfile2.Id
            };

            var success = await _deleteFollowHandler.Consume(JsonConvert.SerializeObject(followEvent));

            Assert.False(success);
        }
    }
}