using System;
using Confluent.Kafka;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Newtonsoft.Json;

namespace Kwetter.Services.ProfileService.Infrastructure.Producer
{
    public class NewProfileEvent : INewProfileEvent, IDisposable
    {
        private readonly IProducer<string, string> _producer;
        private readonly string _topic;

        public NewProfileEvent(IProducer<string, string> producer, string topic)
        {
            _producer = producer;
            _topic = topic;
        }

        public object SendNewProfileEvent(ProfileDto profileDto)
        {
            Message<string, string> message = new()
            {
                Value = JsonConvert.SerializeObject(profileDto),
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