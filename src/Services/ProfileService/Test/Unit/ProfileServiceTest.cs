using System;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Mappings;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Kwetter.Services.ProfileService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using NUnit.Framework;
using Profile = Kwetter.Services.ProfileService.Domain.Entity.Profile;

namespace Kwetter.Services.ProfileService.Test.Unit
{
    public class ProfileServiceTest
    {
        private Application.Services.ProfileService _profileService;
        private ProfileContext _context;
        
        
        [SetUp]
        public void Setup()
        {
            var mockService = new Mock<INewProfileEvent>();
            mockService.Setup(x => x.SendNewProfileEvent(It.IsAny<ProfileDto>()))
                .Returns<Object>(x => new object());
            
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            
            var mapper = mockMapper.CreateMapper();
            
            var dbContext = new DbContextOptionsBuilder<ProfileContext>().UseInMemoryDatabase("Test");
            _context = new ProfileContext(dbContext.Options);
            _context.Database.EnsureCreated();
            _profileService = new Application.Services.ProfileService(_context, mapper, mockService.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task FindExistingProfileById()
        {
            Profile entity = new Profile
            {
                Id = new Guid("d1080a8c-a1b5-4b14-8d30-ee74dccc500d"),
                DisplayName = "John Doe",
                Avatar = "picture.png",
                Description = "Test"
            };

            await _context.Profiles.AddAsync(entity);

            var result = await _profileService.GetProfileAsync(entity.Id);
            
            Assert.NotNull(result);
        }

        [Test]
        public async Task CreateNewProfile()
        {
            Profile entity = new Profile
            {
                Id = new Guid("d1080a8c-a1b5-4b14-8d30-ee74dccc500d"),
                DisplayName = "John Doe",
                Avatar = "picture.png",
                Description = "Test"
            };

            var result =
                await _profileService.CreateProfileAsync(entity.Id, entity.Avatar, entity.Description,
                    entity.DisplayName);
            Assert.IsTrue(result.Success);
            
        }
    }
}