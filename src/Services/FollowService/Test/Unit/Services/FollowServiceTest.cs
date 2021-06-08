using System;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.Common.Mappings;
using Kwetter.Services.FollowService.Domain.Entities;
using Kwetter.Services.FollowService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using Profile = Kwetter.Services.FollowService.Domain.Entities.Profile;

namespace Kwetter.Services.FollowService.Test.Unit.Services
{
    public class FollowServiceTest
    {
        private FollowContext _context;
        private Application.Services.FollowService _followService;

        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); });

            var producer = new Mock<IProducer>();

            var dbContext = new DbContextOptionsBuilder<FollowContext>()
                .UseInMemoryDatabase("KwetterFollowDatabase")
                .Options;
            _context = new FollowContext(dbContext);

            _followService =
                new Application.Services.FollowService(_context, mockMapper.CreateMapper(), producer.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task CreateFollowWhileFollowExist()
        {
            var profile = new Profile
            {
                Id = new Guid(),
                DisplayName = "Roger",
                Avatar = "Test.png"
            };
            var follow = new Follow
            {
                Profile = profile,
                Follower = profile
            };
            _context.Follows.Add(follow);
            _context.Profile.Add(profile);
            await _context.SaveChangesAsync();

            var response = await _followService.CreateFollow(profile.Id, profile.Id);

            Assert.False(response.Success);
            Assert.Null(response.Data);
        }

        [Test]
        public async Task DeleteFollowWhileFollowExist()
        {
            var profile = new Profile
            {
                Id = new Guid(),
                DisplayName = "Roger",
                Avatar = "Test.png"
            };
            var follow = new Follow
            {
                Profile = profile,
                Follower = profile
            };
            _context.Follows.Add(follow);
            _context.Profile.Add(profile);
            await _context.SaveChangesAsync();

            var response = await _followService.DeleteFollow(profile.Id, profile.Id);

            Assert.True(response.Success);
        }

        [Test]
        public async Task CreateFollowWhileFollowDoesNotExist()
        {
            var profile = new Profile
            {
                Id = new Guid(),
                DisplayName = "Roger",
                Avatar = "Test.png"
            };

            _context.Profile.Add(profile);
            await _context.SaveChangesAsync();

            var response = await _followService.CreateFollow(profile.Id, profile.Id);

            Assert.NotNull(response);
            Assert.True(response.Success);
            Assert.NotNull(response.Data);
            Assert.AreEqual(profile.DisplayName, response.Data.DisplayName);
        }

        [Test]
        public async Task DeleteFollowWhileFollowDoesNotExist()
        {
            var profile = new Profile
            {
                Id = new Guid(),
                DisplayName = "Roger",
                Avatar = "Test.png"
            };

            var response = await _followService.DeleteFollow(profile.Id, profile.Id);

            Assert.False(response.Success);
            Assert.Null(response.Data);
        }
    }
}