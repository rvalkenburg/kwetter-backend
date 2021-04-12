using System;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Consumer.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Kwetter.Services.KweetService.Consumer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IHostedService, NewProfileHandler>();

            ConsumerConfig config = new ConsumerConfig();
            config.BootstrapServers = configuration.GetValue<string>("KafkaConsumer:BootstrapServers");
            config.GroupId = Guid.NewGuid().ToString();
            config.AutoOffsetReset = AutoOffsetReset.Earliest;
            services.AddSingleton(option => config);
            
            return services;
        }
    }
}