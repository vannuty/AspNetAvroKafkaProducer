using Confluent.Kafka;
using KafkaAvroProducer.Abstractions.Interfaces.Producers;
using KafkaAvroProducer.Producers.Config;
using System.Text.Json;

namespace KafkaAvroProducer.Kafka.Producers
{
    public class JsonKafkaProducer<T> : IJsonKafkaProducer<T>
    {
        private readonly IProducer<string, string> _producer;
        private readonly KafkaConfig _kafkaConfig;

        protected JsonKafkaProducer(KafkaConfig kafkaConfig)
        {
            _kafkaConfig = kafkaConfig;
            if (_kafkaConfig == null)
                throw new ArgumentNullException(nameof(_kafkaConfig));

            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaConfig.Server,
                SaslUsername = _kafkaConfig.APIKey,
                SaslPassword = _kafkaConfig.APISecret,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                RequestTimeoutMs = 45000,
            };

            _producer = new ProducerBuilder<string, string>(config).Build();

        }

        public async Task<PersistenceStatus> PublishMessageAsync(string topic, string key, T message)
        {
            try
            {
                var response = await _producer.ProduceAsync(topic,
                new Message<string, string>
                {
                    Key = key,
                    Value = JsonSerializer.Serialize(message)
                });

                return response.Status;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return PersistenceStatus.NotPersisted;
            }
        }
    }
}
