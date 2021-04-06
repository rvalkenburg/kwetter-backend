using System.Threading.Tasks;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;

namespace Kwetter.Services.KweetService.Application.Services
{
    public class KweetService : IKweetService
    {
        public Task<Response<KweetDto>> CreateKweetAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}