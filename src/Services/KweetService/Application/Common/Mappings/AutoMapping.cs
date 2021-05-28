using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Entities.Kweet, KweetDto>()
                .ForMember(x => x.LikeCount, o => o.MapFrom(x => x.Likes.Count()))
                .ForMember(x => x.IsLiked, o => o.MapFrom(x => x.Likes.Any(c => c.ProfileId == x.Profile.Id)));

            CreateMap<Domain.Entities.Profile, ProfileDto>();
        }
    }
}