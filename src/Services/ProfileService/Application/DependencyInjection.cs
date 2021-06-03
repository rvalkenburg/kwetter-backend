using System;
using System.Collections.Generic;
using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Interfaces.Handlers;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Kwetter.Services.ProfileService.Application.EventHandlers;
using Kwetter.Services.ProfileService.Application.EventHandlers.User;
using Kwetter.Services.ProfileService.Application.Events;
using Kwetter.Services.ProfileService.Domain.Entity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Kwetter.Services.ProfileService.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddScoped<ICreateUserHandler, CreateUserHandler>();
            
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
            handlers.Add("Create-User", serviceProvider.GetRequiredService<ICreateUserHandler>());

            consumer.AddSubscriber(handlers);
            
            services.AddHostedService(sp => consumer);

            return services;
        }
    }
}