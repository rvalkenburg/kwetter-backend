using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Kwetter.Services.KweetService.Application.EventHandlers
{
    public class NewProfileHandler : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly IProfileService _services;
        
        public NewProfileHandler(IConsumer<Ignore, string> consumer, IProfileService services)
        {
            _services = services;
            _consumer = consumer;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = _consumer.Consume(stoppingToken);

                if (consumeResult != null)
                {
                    ProfileDto profileDto = JsonConvert.DeserializeObject<ProfileDto>(consumeResult.Message.Value);
                     await _services.AddProfile(profileDto);
                    Console.WriteLine($"Consumed message '{consumeResult.Message.Value}' at: '{consumeResult.TopicPartitionOffset}'.");
                }
            }
        }
        
        public override void Dispose()
        {
            _consumer.Dispose();
            base.Dispose();
        }
    }
}