using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Interfaces.Handlers;
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
            };

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            Consumer consumer = new Consumer(config);
            consumer.AddSubscriber("Create-Follow", serviceProvider.GetRequiredService<ICreateFollowHandler>());
            consumer.AddSubscriber("Delete-Follow", serviceProvider.GetRequiredService<IDeleteFollowHandler>());
            consumer.AddSubscriber("Update-Profile", serviceProvider.GetRequiredService<IUpdateProfileHandler>());
            consumer.AddSubscriber("Create-Profile", serviceProvider.GetRequiredService<ICreateProfileHandler>());
            services.AddHostedService(sp => consumer);
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}