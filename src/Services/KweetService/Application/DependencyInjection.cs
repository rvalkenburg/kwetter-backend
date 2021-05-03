using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.EventHandlers;
using Kwetter.Services.KweetService.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.KweetService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IKweetService, Application.Services.KweetService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<ILikeService, LikeService>();
            
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetValue<string>("ProducerConfig:BootstrapServers"),
                GroupId = configuration.GetValue<string>("ProducerConfig:GroupId"),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
            };
            
            var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            
            consumer.Subscribe("ProfileUpdated");
            services.AddHostedService(sp => new NewProfileHandler(consumer, services.BuildServiceProvider().GetRequiredService<IProfileService>()));
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}