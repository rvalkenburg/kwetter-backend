using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.EventHandlers;
using Kwetter.Services.KweetService.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.KweetService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IKweetService, Application.Services.KweetService>();
            services.AddScoped<IProfileService, ProfileService>();
            
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "InNeedOfProfiles",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            
            var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            
            consumer.Subscribe("ProfileUpdated");
            services.AddHostedService(sp => new NewProfileHandler(consumer, services.BuildServiceProvider().GetRequiredService<IProfileService>()));
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}