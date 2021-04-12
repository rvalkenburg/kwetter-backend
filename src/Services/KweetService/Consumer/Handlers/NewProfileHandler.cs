using System;
using System.Runtime.InteropServices.ComTypes;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Kwetter.Services.KweetService.Application.Common.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;

namespace Kwetter.Services.KweetService.Consumer.Handlers
{
    public class NewProfileHandler : BackgroundService
    {
        private readonly ConsumerConfig _config;
        
        public NewProfileHandler(ConsumerConfig config)
        {
            _config = config;
        }
        
        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Task.Run(() => Start(stoppingToken));
            return Task.CompletedTask;
        }
 
        private void Start(CancellationToken stoppingToken)
        {
            using (var c = new ConsumerBuilder<Ignore, string>(_config).Build())
            {
                c.Subscribe("NewProfileEvent");
 
                CancellationTokenSource cts = new CancellationTokenSource();
                Console.CancelKeyPress += (_, e) => {
                    e.Cancel = true; // prevent the process from terminating.
                    cts.Cancel();
                };
 
                try
                {
                    while (true)
                    {
                        try
                        {
                            var cr = c.Consume(cts.Token);
                            Console.WriteLine($"Consumed message '{cr.Value}' at: '{cr.TopicPartitionOffset}'.");
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Error occured: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    c.Close();
                }
            }
        }
    }
}