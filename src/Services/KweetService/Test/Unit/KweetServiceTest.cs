using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Mappings;
using Kwetter.Services.KweetService.Domain.Entities;
using Kwetter.Services.KweetService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Profile = Kwetter.Services.KweetService.Domain.Entities.Profile;

namespace Kwetter.Services.KweetService.Test.Unit
{
    public class KweetServiceTest
    {
        private Application.Services.KweetService _kweetService;
        private KweetContext _context;
        
        [SetUp]
        public void Setup()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapping());
            });
            
            var mapper = mockMapper.CreateMapper();
            
            var dbContext = new DbContextOptionsBuilder<KweetContext>().UseInMemoryDatabase("Test");
            _context = new KweetContext(dbContext.Options);
            _context.Database.EnsureCreated();
            _kweetService = new Application.Services.KweetService(_context, mapper);
        }

        [TearDown]
        public void TearDown()
        {
            _context.Database.EnsureDeleted();
        }

        [Test]
        public async Task CreateNewKweet()
        {
            Profile profile = new Profile
            {
                Id = new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"),
                DisplayName = "John Doe",
                Avatar = "Avatar.png"
                
            };

            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            
            var response = await _kweetService.CreateKweetAsync(new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"), "Test");
            
            Assert.True(response.Success);
        }
        
        [Test]
        public async Task GetPaginatedKweets()
        {
            Profile profile = new Profile
            {
                Id = new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"),
                DisplayName = "John Doe",
                Avatar = "Avatar.png"
                
            };
        
            await _context.Profiles.AddAsync(profile);
            await _context.SaveChangesAsync();
            
            await _kweetService.CreateKweetAsync(new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"), "Test");
            await _kweetService.CreateKweetAsync(new Guid("0001aec8-1fdd-42ae-b552-9cbfb693767e"), "Test");
            
            var response = await _kweetService.GetPaginatedKweetsByProfile(0, 1, profile.Id);
            
            Assert.AreEqual(1, response.Data.Count());
        }
    }
}