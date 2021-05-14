using System.Collections.Generic;
using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Services;
using Kwetter.Services.KweetService.Application.EventHandlers;
using Kwetter.Services.KweetService.Application.EventHandlers.Follow;
using Kwetter.Services.KweetService.Application.EventHandlers.Profile;
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
            services.AddScoped<ILikeService, LikeService>();
            
            services.AddScoped<ICreateProfileHandler, CreateProfileHandler>();
            services.AddScoped<IUpdateProfileHandler, UpdateProfileHandler>();

            services.AddScoped<IDeleteFollowHandler, DeleteFollowHandler>();
            services.AddScoped<ICreateFollowHandler, CreateFollowHandler>();
            
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetValue<string>("ProducerConfig:BootstrapServers"),
                GroupId = configuration.GetValue<string>("ProducerConfig:GroupId"),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
                AllowAutoCreateTopics = true
            };

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            Consumer consumer = new Consumer(config);
            
            Dictionary<string, IHandler> handlers = new Dictionary<string, IHandler>();
            handlers.Add("Create-Follow", serviceProvider.GetRequiredService<ICreateFollowHandler>());
            handlers.Add("Delete-Follow", serviceProvider.GetRequiredService<IDeleteFollowHandler>());
            handlers.Add("Update-Profile", serviceProvider.GetRequiredService<IUpdateProfileHandler>());
            handlers.Add("Create-Profile", serviceProvider.GetRequiredService<ICreateProfileHandler>());
            consumer.AddSubscriber(handlers);
            
            services.AddHostedService(sp => consumer);
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}