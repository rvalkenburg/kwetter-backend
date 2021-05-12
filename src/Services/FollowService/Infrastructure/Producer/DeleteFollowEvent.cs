using System;
using Confluent.Kafka;
using Kwetter.Services.FollowService.Application.Common.Interfaces;
using Kwetter.Services.FollowService.Application.Common.Models;
using Newtonsoft.Json;

namespace Kwetter.Services.FollowService.Infrastructure.Producer
{
    public class DeleteFollowEvent: IDisposable, IDeleteFollowEvent
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public DeleteFollowEvent(IProducer<string, string> producer, string topic)
        {
            _producer = producer;
            _topic = topic;
        }

        public Object SendDeleteFollowEvent(FollowEvent followEvent)
        {
            Message<string, string> message = new()
            {
                Value = JsonConvert.SerializeObject(followEvent),
            };

            try
            {
                return _producer.ProduceAsync(_topic, message);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Oops, something went wrong: {e}");
            }

            return null;
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}