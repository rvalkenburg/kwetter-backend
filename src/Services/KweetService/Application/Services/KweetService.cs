using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;
using Kwetter.Services.KweetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class KweetService : IKweetService
    {
        private readonly IKweetContext _context;
        private readonly IMapper _mapper;
        
        public KweetService(IKweetContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<Response<KweetDto>> CreateKweetAsync(Guid profileId, string message)
        {
            Response<KweetDto> response = new Response<KweetDto>();
            
            var kweet = new Kweet
            {
                Id = Guid.NewGuid(),
                ProfileId = profileId,
                Message = message,
            };
            
            await _context.Kweets.AddAsync(kweet);
            bool success = await _context.SaveChangesAsync() > 0;

            if (success)
            {
                response.Success = true;
                response.Data = _mapper.Map<KweetDto>(kweet);
            }

            return response;
        }

        public async Task<Response<IEnumerable<KweetDto>>> GetPaginatedKweets(int pageNumber, int pageSize)
        {
            Response<IEnumerable<KweetDto>> response = new();

            var entities = await _context.Kweets
                .Skip(pageNumber * pageSize)
                .Take(pageSize)
                .ToListAsync();

            if (entities == null) return response;
            
            response.Data = _mapper.Map<IEnumerable<KweetDto>>(entities);
            response.Success = true;

            return response;
        }
    }
}