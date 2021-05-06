using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.FollowService.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FollowContext>(
                options => options.UseSqlServer(configuration.GetConnectionString("ConnectionString"))
                    .UseLazyLoadingProxies()
            );

            services.AddScoped<IFollowContext>(provider => provider.GetService<FollowContext>());

            return services;
        }
    }
}