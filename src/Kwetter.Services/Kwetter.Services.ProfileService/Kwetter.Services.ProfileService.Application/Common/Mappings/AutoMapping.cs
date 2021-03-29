using System.Collections.Generic;
using AutoMapper;
using Kwetter.Services.ProfileService.Application.Common.Models;

namespace Kwetter.Services.ProfileService.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Entity.Profile, ProfileDto>(); // means you want to map from User to UserDTO
            CreateMap<Domain.Entity.Profile, IEnumerable<ProfileDto>>(); // means you want to map from User to UserDTO
        }
    }
}