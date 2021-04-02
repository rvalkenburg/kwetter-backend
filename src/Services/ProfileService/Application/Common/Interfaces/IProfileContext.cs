using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Profile = Kwetter.Services.ProfileService.Domain.Entity.Profile;

namespace Kwetter.Services.ProfileService.Application.Common.Interfaces
{
    public interface IProfileContext
    {
        DbSet<Profile> Profiles { get; set; }
        Task<int> SaveChangesAsync();
    }
}