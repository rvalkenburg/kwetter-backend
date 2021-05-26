using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.ProfileService.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProfileContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped<IProfileContext>(provider => provider.GetService<ProfileContext>());

            return services;
        }
    }
}