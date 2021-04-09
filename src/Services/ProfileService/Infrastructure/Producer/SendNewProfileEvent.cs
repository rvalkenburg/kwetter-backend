using System;
using System.Text.Json.Serialization;
using Confluent.Kafka;
using Kwetter.Services.ProfileService.Application.Common.Interfaces;
using Kwetter.Services.ProfileService.Application.Common.Models;
using Newtonsoft.Json;

namespace Kwetter.Services.ProfileService.Infrastructure.Producer
{
    public class NewProfileEvent : INewProfileEvent
    {
        private readonly ProducerConfig config = new()
        {
            BootstrapServers = "localhost:9092"
        };
        
        public object SendNewProfileEvent(ProfileDto message)
        {
            string serializedProfile = JsonConvert.SerializeObject(message);
            
            using (var producer = 
                new ProducerBuilder<Null, string>(config).Build())
            {
                try
                {
                    return producer.ProduceAsync("NewProfileEvent", new Message<Null, string> { Value = serializedProfile })
                        .GetAwaiter()
                        .GetResult();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Oops, something went wrong: {e}");
                }
            }
            return null;
        }
    }
}