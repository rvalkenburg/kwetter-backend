using System.Reflection;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.EventHandlers;
using Kwetter.Services.KweetService.Application.Events;
using Kwetter.Services.KweetService.Application.Services;
using Microsoft.EntityFrameworkCore;
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
            services.AddScoped<IFollowService, FollowService>();
            
            services.AddScoped<IFollowHandler, NewFollowHandler>();
            //services.AddScoped<IHandler, UpdateProfileHandler>();
            
            ConsumerConfig config = new ConsumerConfig
            {
                BootstrapServers = configuration.GetValue<string>("ProducerConfig:BootstrapServers"),
                GroupId = configuration.GetValue<string>("ProducerConfig:GroupId"),
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false,
            };

            ServiceProvider serviceProvider = services.BuildServiceProvider();

            var followCreatedEvent = serviceProvider.GetRequiredService<IFollowHandler>();
            
            Consumer consumer = new Consumer(config);
            consumer.AddSubscriber("FollowCreated", followCreatedEvent);
            services.AddHostedService(sp => consumer);
            
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}

// IConsumer<Ignore, string> consumer = new ConsumerBuilder<Ignore, string>(config).Build();
// consumer.Subscribe(new List<string> {"ProfileUpdated", "FollowCreated"});

// switch (topic)
// {
//     case "FollowCreated":
//         return new NewFollowHandler(_followService);
//     case "ProfileUpdated":
//         return new UpdateProfileHandler(_profileService);
//     default:
//         return null;
// }