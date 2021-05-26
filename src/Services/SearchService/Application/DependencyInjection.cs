using System.Collections.Generic;
using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.SearchService.Application.Common.Interfaces;
using Kwetter.Services.SearchService.Application.EventHandlers;
using Kwetter.Services.SearchService.Application.EventHandlers.Follow;
using Kwetter.Services.SearchService.Application.EventHandlers.Profile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.SearchService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISearchService, Services.SearchService>();
            
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