using Kwetter.Services.KweetService.Consumer.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kwetter.Services.KweetService.Consumer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConsumer(this IServiceCollection services)
        {
            services.AddSingleton<IHostedService, NewProfileHandler>();
            return services;
        }
    }
}