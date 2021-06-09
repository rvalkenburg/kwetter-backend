using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.SearchService.Application.Common.Mappings;
using NUnit.Framework;

namespace Kwetter.Services.SearchService.Test.Unit.Services
{
    public class SearchServiceTest : BaseTest
    {
        private Application.Services.SearchService _searchService;

        [SetUp]
        public void Setup()
        {
            Init();

            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); });
            var mapper = mockMapper.CreateMapper();

            _searchService = new Application.Services.SearchService(Context, mapper);
        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }

        [Test]
        public async Task SearchProfilesByEmptyName()
        {
            var response = await _searchService.GetPaginatedSearch(10, 0, "", new Guid());
            Assert.True(response.Success);
            Assert.AreEqual(0, response.Data.Count());
        }

        [Test]
        public async Task SearchProfilesByCorrectName()
        {
            var response = await _searchService.GetPaginatedSearch(10, 0, "Roger", new Guid());
            Assert.True(response.Success);
            Assert.AreEqual(1, response.Data.Count());
        }

        [Test]
        public async Task SearchProfilesByPartialFullName()
        {
            var response = await _searchService.GetPaginatedSearch(10, 0, "Rog", new Guid());
            Assert.True(response.Success);
            Assert.AreEqual(0, response.Data.Count());
        }
    }
}