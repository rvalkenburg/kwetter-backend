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

        public KafkaProducer(ProducerConfig config)
        {
            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        public void Dispose()
        {
            _producer.Dispose();
        }

        public async Task<bool> Send<T>(string topic, Event<T> @event)
        {
            try
            {
                var message = new Message<string, string> {Value = JsonConvert.SerializeObject(@event.Data)};
                await _producer.ProduceAsync(topic, message);
                return true;
            }
            catch (Exception)
            {
                Console.WriteLine("Oops, something went wrong");
            }

            return false;
        }
    }
}