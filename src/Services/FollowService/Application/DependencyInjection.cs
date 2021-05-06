using System.Reflection;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.FollowService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFollowService, Application.Services.FollowService>();
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}