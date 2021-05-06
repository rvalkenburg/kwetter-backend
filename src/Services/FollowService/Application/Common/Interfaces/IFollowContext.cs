using System.Threading.Tasks;
using Kwetter.Services.FollowService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.FollowService.Application.Common.Interfaces
{
    public interface IFollowContext
    {
        DbSet<Follow> Follows { get; set; }     
        DbSet<Profile> Profile { get; set; }        

        Task<int> SaveChangesAsync();
    }
}