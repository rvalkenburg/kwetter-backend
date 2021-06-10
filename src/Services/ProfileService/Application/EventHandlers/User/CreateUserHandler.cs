using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Kwetter.Services.ProfileService.Application.Events;
using Newtonsoft.Json;
using Profile = Kwetter.Services.ProfileService.Domain.Entity.Profile;

namespace Kwetter.Services.ProfileService.Application.EventHandlers.User
{
    public class CreateUserHandler : ICreateUserHandler
    {
        private readonly IProfileContext _context;
        private readonly IMapper _mapper;
        private readonly IProducer _producer;

        public CreateUserHandler(IProfileContext context, IMapper mapper, IProducer producer)
        {
            _context = context;
            _mapper = mapper;
            _producer = producer;
        }
        
        public async Task<bool> Consume(string message)
        {
            UserEvent userEvent = JsonConvert.DeserializeObject<UserEvent>(message);

            if (userEvent == null) return false;
            
            Profile profile = new Profile
            {
                Id = userEvent.Id,
                Avatar = userEvent.Avatar,
                DisplayName = userEvent.DisplayName,
                DateOfCreation = userEvent.DateOfCreation,
                Email = userEvent.Email,
                GoogleId = userEvent.GoogleId
            };

            await _context.Profiles.AddAsync(profile);
            bool success = await _context.SaveChangesAsync() > 0;

            if (!success) return false;
            
            ProfileDto profileDto = _mapper.Map<ProfileDto>(profile);
            CreateProfileEvent(profileDto);
            
            return true;
        }
        
        private void CreateProfileEvent(ProfileDto profileDto)
        {
            if (profileDto != null)
            {
                Event<ProfileDto> createProfileEvent = new Event<ProfileDto>
                {
                    Data = profileDto
                };
                    
                _producer.Send("Create-Profile", createProfileEvent);
            }
        }
    }
}