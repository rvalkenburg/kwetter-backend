using Kwetter.Services.AuthorizationService.Application.Common.Interfaces;
using Kwetter.Services.AuthorizationService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.AuthorizationService.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AuthContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("ConnectionString")));

            services.AddScoped<IAuthContext>(provider => provider.GetService<AuthContext>());
            
            return services;
        }
    }
}