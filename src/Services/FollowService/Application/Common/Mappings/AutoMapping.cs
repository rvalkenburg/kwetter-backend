using Kwetter.Services.FollowService.Application.Common.Models;
using Kwetter.Services.FollowService.Domain.Entities;
using Profile = AutoMapper.Profile;

namespace Kwetter.Services.FollowService.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Follow, FollowDto>()
                .ForMember(x => x.Avatar, opt => opt.MapFrom(o => o.Follower.Avatar))
                .ForMember(x => x.DisplayName, opt => opt.MapFrom(o => o.Follower.DisplayName))
                .ForMember(x => x.Id, opt => opt.MapFrom(o => o.Follower.Id));
        }
    }
}