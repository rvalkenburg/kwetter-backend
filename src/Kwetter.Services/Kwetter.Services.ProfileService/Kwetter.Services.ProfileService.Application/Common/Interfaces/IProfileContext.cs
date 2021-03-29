using System.Threading;
using System.Threading.Tasks;
using Kwetter.Services.ProfileService.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces
{
    public interface IProfileContext
    {
        DbSet<Profile> Profiles { get; set; }
        Task<int> SaveChangesAsync();
    }
}