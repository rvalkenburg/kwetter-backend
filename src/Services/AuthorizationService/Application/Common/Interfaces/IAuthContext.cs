using System.Threading.Tasks;
using Kwetter.Services.AuthorizationService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.AuthorizationService.Application.Common.Interfaces
{
    public interface IAuthContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}