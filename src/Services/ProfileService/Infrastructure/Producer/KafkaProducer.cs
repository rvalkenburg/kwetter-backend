using System;
using System.Threading.Tasks;
using Confluent.Kafka;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Newtonsoft.Json;

namespace Kwetter.Services.ProfileService.Infrastructure.Producer
{
    public class KafkaProducer : IProducer, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        private Message<string, string> _message;

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public async Task<bool> Send<T>(string topic, Event<T> @event)
        {
            try
            {
                _message = new Message<string, string> {Value = JsonConvert.SerializeObject(@event.Data)};
                await _producer.ProduceAsync(topic, _message);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine($"Oops, something went wrong");
            }

            return false;
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}