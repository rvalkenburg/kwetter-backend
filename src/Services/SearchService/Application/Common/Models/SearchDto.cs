using System;

namespace Kwetter.Services.SearchService.Application.Common.Models
{
    public class SearchDto
    {
        public Guid Id { get; set; }
        public string DisplayName { get; set; }
        public string Avatar { get; set; }
        public bool Status { get; set; }
    }
}