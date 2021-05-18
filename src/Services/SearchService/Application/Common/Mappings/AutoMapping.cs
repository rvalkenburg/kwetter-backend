using AutoMapper;
using Kwetter.Services.SearchService.Application.Common.Models;

namespace Kwetter.Services.SearchService.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Entities.Profile, SearchDto>();

        }
    }
}