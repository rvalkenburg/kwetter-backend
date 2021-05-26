using AutoMapper;
using Kwetter.Services.AuthorizationService.Application.Common.Models;
using Kwetter.Services.AuthorizationService.Application.Events;
using Kwetter.Services.AuthorizationService.Domain.Entities;

namespace Kwetter.Services.AuthorizationService.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<User, UserEvent>(); // means you want to map from User to UserDTO
            CreateMap<User, UserDto>(); // means you want to map from User to UserDTO


        }
    }
}