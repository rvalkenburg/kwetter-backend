using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace Kwetter.Services.KweetService.Consumer.Handlers
{
    public class NewProfileHandler : IHostedService
    {
        private readonly string topic = "NewProfileEvent";
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            var conf = new ConsumerConfig
            {
                GroupId = "NewProfileEvent",
                BootstrapServers = "localhost:9092",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using var builder = new ConsumerBuilder<Ignore, 
                string>(conf).Build();
            builder.Subscribe(topic);
            var cancelToken = new CancellationTokenSource();
            try
            {
                while (true)
                {
                    var consumer = builder.Consume(cancelToken.Token);
                    if (consumer != null)
                    {
                        //Create new profile
                    }

                    Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
                }
            }
            catch (Exception)
            {
                builder.Close();
            }

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}