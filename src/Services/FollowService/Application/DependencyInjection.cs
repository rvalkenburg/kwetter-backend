using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.EventHandlers;
using Kwetter.Services.FollowService.Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.FollowService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFollowService, Application.Services.FollowService>();
            services.AddScoped<IProfileService, ProfileService>();
            
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
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}