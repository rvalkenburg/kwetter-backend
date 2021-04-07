using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Mappings;
using Kwetter.Services.KweetService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;

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
            var response = await _kweetService.CreateKweetAsync(new Guid(), "Test");
            
            Assert.True(response.Success);
        }
        
        [Test]
        public async Task GetPaginatedKweets()
        {
            await _kweetService.CreateKweetAsync(new Guid(), "Test");
            await _kweetService.CreateKweetAsync(new Guid(), "Test");
            await _kweetService.CreateKweetAsync(new Guid(), "Test");
            await _kweetService.CreateKweetAsync(new Guid(), "Test");
            
            var response = await _kweetService.GetPaginatedKweets(0, 1);
            
            Assert.AreEqual(1, response.Data.Count());
        }
    }
}