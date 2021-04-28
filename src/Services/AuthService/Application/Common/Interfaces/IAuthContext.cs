using System.Threading.Tasks;
using Kwetter.Services.AuthService.Domain;
using Microsoft.EntityFrameworkCore;

namespace Kwetter.Services.AuthService.Application.Common.Interfaces
{
    public interface IAuthContext
    {
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync();
    }
}