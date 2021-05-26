using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.SearchService.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SearchContext>(
                options => options.UseNpgsql(configuration.GetConnectionString("ConnectionString"))
                .UseLazyLoadingProxies()
                );
            services.AddScoped<ISearchContext>(provider => provider.GetService<SearchContext>());

            return services;
        }
    }
}