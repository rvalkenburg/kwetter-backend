using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Mappings;
using NUnit.Framework;
using Profile = Kwetter.Services.KweetService.Domain.Entities.Profile;

namespace Kwetter.Services.KweetService.Test.Unit.Services
{
    public class KweetServiceTest : BaseTest
    {
        private Application.Services.KweetService _kweetService;

        [SetUp]
        public void Setup()
        {
            Init();

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); });

            var mapper = mockMapper.CreateMapper();

            _kweetService = new Application.Services.KweetService(Context, mapper);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        [Test]
        public async Task CreateNewKweet()
        {
            var profile = new Profile
            {
                Id = new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"),
                DisplayName = "John Doe",
                Avatar = "Avatar.png"
            };

            await Context.Profiles.AddAsync(profile);
            await Context.SaveChangesAsync();

            var response = await _kweetService.CreateKweetAsync(profile.Id, "Test");

            Assert.True(response.Success);
        }

        [Test]
        public async Task GetPaginatedKweets()
        {
            var profile = new Profile
            {
                Id = new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"),
                DisplayName = "John Doe",
                Avatar = "Avatar.png"
            };

            await Context.Profiles.AddAsync(profile);
            await Context.SaveChangesAsync();

            await _kweetService.CreateKweetAsync(new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"), "Test");
            await _kweetService.CreateKweetAsync(new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"), "Test");

            var response = await _kweetService.GetPaginatedKweetsByProfile(0, 1, profile.Id);

            Assert.AreEqual(1, response.Data.Count());
        }
    }
}