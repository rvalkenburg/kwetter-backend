using System.Collections.Generic;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Mappings
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<Domain.Entities.Kweet, KweetDto>();
            CreateMap<Domain.Entities.Kweet, IEnumerable<KweetDto>>();

        }
    }
}