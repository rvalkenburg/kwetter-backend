using Confluent.Kafka;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Infrastructure.Producer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Kwetter.Services.FollowService.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ProducerConfig config = new ProducerConfig
            {
                BootstrapServers = configuration.GetValue<string>("ProducerConfig:BootstrapServers"),
            };
            var producer = new ProducerBuilder<string, string>(config).Build();
            services.AddSingleton<INewFollowEvent>(_ => new NewFollowEvent(producer, "FollowCreated"));
            services.AddSingleton<IDeleteFollowEvent>(_ => new DeleteFollowEvent(producer, "FollowDelete"));
            return services;
        }
    }
}