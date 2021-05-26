using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kwetter.Services.ProfileService.Application.Common.Interfaces.Handlers;
using Microsoft.Extensions.Hosting;

namespace Kwetter.Services.ProfileService.Application.EventHandlers
{
    public class Consumer : BackgroundService
    {
        private readonly IConsumer<Ignore, string> _consumer;
        private readonly ConcurrentDictionary<string, IHandler> _topicDictionary;

        public Consumer(ConsumerConfig config)
        {
            _consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            ;
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

                    bool success = await handler.Consume(consumeResult.Message.Value);

                    if (success)
                    {
                        _consumer.Commit();
                    }
                }
                catch (ConsumeException)
                {
                    Console.WriteLine("Something went wrong");
                }
            }
        }

        public override void Dispose()
        {
            _consumer.Dispose();
            base.Dispose();
        }

        public void AddSubscriber(Dictionary<string, IHandler> handlers)
        {
            foreach (var handler in handlers.Where(handler => !_topicDictionary.ContainsKey(handler.Key)))
            {
                _topicDictionary.TryAdd(handler.Key, handler.Value);
            }

            _consumer.Subscribe(handlers.Keys);
        }
    }
}