using System.Collections.Generic;
using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.FollowService.Application.EventHandlers;
using Kwetter.Services.FollowService.Application.EventHandlers.Profile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.FollowService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IFollowService, Application.Services.FollowService>();
            
            services.AddScoped<ICreateProfileHandler, CreateProfileHandler>();
            services.AddScoped<IUpdateProfileHandler, UpdateProfileHandler>();
            
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetValue<string>("ProducerConfig:BootstrapServers"),
                GroupId = configuration.GetValue<string>("ProducerConfig:GroupId"),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
            };
            
            ServiceProvider serviceProvider = services.BuildServiceProvider();

            Consumer consumer = new Consumer(config);

            Dictionary<string, IHandler> handlers = new Dictionary<string, IHandler>
            {
                {"Update-Profile", serviceProvider.GetRequiredService<IUpdateProfileHandler>()},
                {"Create-Profile", serviceProvider.GetRequiredService<ICreateProfileHandler>()}
            };
            consumer.AddSubscriber(handlers);
            
            services.AddHostedService(sp => consumer);
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}