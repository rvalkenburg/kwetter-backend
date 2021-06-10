using System;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Mappings;
using Kwetter.Services.ProfileService.Application.EventHandlers.User;
using Kwetter.Services.ProfileService.Application.Events;
using Moq;
using Newtonsoft.Json;
using NUnit.Framework;

namespace Kwetter.Services.ProfileService.Test.Unit.EventHandlers
{
    public class CreateUserHandlerTest : BaseTest
    {
        private CreateUserHandler _createUserHandler;
        
        [SetUp]
        public void Setup()
        {
            Init();
            var mockMapper = new MapperConfiguration(cfg => { cfg.AddProfile(new AutoMapping()); });
            var mapper = mockMapper.CreateMapper();

            var producer = new Mock<IProducer>();
            
            _createUserHandler = new CreateUserHandler(Context, mapper, producer.Object);

        }

        [TearDown]
        public void TearDown()
        {
            Context.Database.EnsureDeleted();
        }
        
        [Test]
        public async Task CreateProfileWhileGoogleIdIsNotExisting()
        {
            UserEvent userEvent = new UserEvent
            {
                Id = new Guid(),
                DisplayName = "Roger",
                Avatar = "Test.png",
                DateOfCreation = DateTime.Now,
                Description = "Description",
                Email = "roger@kwetter.nl",
                GoogleId = "00000001"
            };
            
            bool success = await _createUserHandler.Consume(JsonConvert.SerializeObject(userEvent));
            
            Assert.True(success);
        }
    }
}