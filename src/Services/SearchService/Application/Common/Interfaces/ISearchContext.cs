using System.Threading.Tasks;
using Kwetter.Services.SearchService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.SearchService.Application.Common.Interfaces
{
    public interface ISearchContext
    {
        DbSet<Profile> Profiles { get; set; }
        DbSet<Follow> Follow { get; set; }
        Task<int> SaveChangesAsync();
    }
}