using System;
using System.Threading.Tasks;
using Kwetter.Services.SearchService.Application.Common.Models;

namespace Kwetter.Services.SearchService.Application.Common.Interfaces
{
    public interface ISearchService
    {
        Task<PaginationResponse<SearchDto>> GetPaginatedSearch(int pageSize, int pageNumber, string name, Guid profileId);

    }
}