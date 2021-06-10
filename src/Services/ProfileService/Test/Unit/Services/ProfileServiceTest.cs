using System;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Mappings;
using Moq;
using NUnit.Framework;
using Profile = Kwetter.Services.ProfileService.Domain.Entity.Profile;

namespace Kwetter.Services.ProfileService.Test.Unit.Services
{
    public class ProfileServiceTest : BaseTest
    {
        private Application.Services.ProfileService _profileService;

        [SetUp]
        public void Setup()
        {
            Init();

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); });
            var mapper = mockMapper.CreateMapper();

            var producer = new Mock<IProducer>();

            _profileService = new Application.Services.ProfileService(Context, mapper, producer.Object);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        [Test]
        public async Task FindExistingProfileById()
        {
            var entity = new Profile
            {
                Id = new Guid("d1080a8c-a1b5-4b14-8d30-ee74dccc500d"),
                DisplayName = "John Doe",
                Avatar = "picture.png",
                Description = "Test"
            };

            Context.Profiles.Add(entity);
            await Context.SaveChangesAsync();
            var result = await _profileService.GetProfileAsync(entity.Id);

            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.AreEqual(entity.Id, result.Data.Id);
        }
        
        [Test]
        public async Task FindNotExistingProfileById()
        {
            var entity = new Profile
            {
                Id = new Guid("d1080a8c-a1b5-4b14-8d30-ee74dccc500d"),
                DisplayName = "John Doe",
                Avatar = "picture.png",
                Description = "Test"
            };

            Context.Profiles.Add(entity);
            await Context.SaveChangesAsync();
            var result = await _profileService.GetProfileAsync(entity.Id);

            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }
    }
}