using System.Threading.Tasks;
using Kwetter.Services.KweetService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IKweetContext
    {
        DbSet<Kweet> Kweets { get; set; }        
        DbSet<Like> Likes { get; set; }
        DbSet<Profile> Profiles { get; set; }
        Task<int> SaveChangesAsync();
    }
}