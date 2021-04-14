using System;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Kwetter.Services.KweetService.Consumer
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddConsumer(this IServiceCollection services, IConfiguration configuration)
        {

            
            return services;
        }
    }
}