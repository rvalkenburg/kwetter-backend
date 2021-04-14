using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Common.Interfaces
{
    public interface IProfileService 
    {
        Task AddProfile(ProfileDto profileDto);
    }
}