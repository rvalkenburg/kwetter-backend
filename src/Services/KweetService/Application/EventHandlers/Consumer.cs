using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kwetter.Services.KweetService.Application.Common.Interfaces;
using Microsoft.Extensions.Hosting;

namespace Kwetter.Services.KweetService.Application.EventHandlers
{
    public class Consumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ConcurrentDictionary<string, IHandler> _topicDictionary;
        public Consumer(ConsumerConfig config)
        {
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();;
            _topicDictionary = new ConcurrentDictionary<string, IHandler>();
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);

                    if (consumeResult == null) continue;
                    if (!_topicDictionary.TryGetValue(consumeResult.Topic, out IHandler handler))
                    {
                        Console.WriteLine("Topic was not found");
                        continue;
                    }

                    bool success = true;
                    //bool success = await handler.Consume(consumeResult.Message.Value);
                        
                    if (success)
                    {
                        _consumer.Commit();
                    }
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error occured: {e.Error.Reason}");
                }
            }
        }
        
        public override void Dispose()
        {
            _consumer.Dispose();
            base.Dispose();
        }
        
        public void AddSubscriber(string topic, IHandler handler)
        {
            if (_topicDictionary.ContainsKey(topic))
            {
                //throw error
            }
            else
            {
                _topicDictionary.TryAdd(topic, handler);
                _consumer.Subscribe(topic);
            }
            
        }
    }
}