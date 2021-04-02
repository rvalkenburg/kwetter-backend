using System.Collections.Generic;

namespace Kwetter.Services.ProfileService.Application.Common.Models
{
    public class PaginationResponse<T>
    {
        public IEnumerable<T> Data { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public bool Success { get; set; }
    }
}