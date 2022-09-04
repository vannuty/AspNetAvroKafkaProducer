using Confluent.Kafka;
using Confluent.SchemaRegistry;
using KafkaAvroProducer.Producers.Config;

namespace KafkaAvroProducer.Kafka.Producers
{
    public abstract class GenericKafkaProducer<T> where T : new()
    {
        protected readonly IProducer<string, T> Producer;

        protected readonly CachedSchemaRegistryClient SchemaRegistry;

        private readonly KafkaConfig _kafkaConfig;
        protected abstract IAsyncSerializer<T> Serializer { get; }
        protected abstract string PublishTopic { get; }
        protected abstract string PublishKey { get; }

        protected GenericKafkaProducer(KafkaConfig kafkaConfig)
        {
            _kafkaConfig = kafkaConfig;
            var config = new ProducerConfig
            {
                BootstrapServers = _kafkaConfig.Server,
                SaslUsername = _kafkaConfig.APIKey,
                SaslPassword = _kafkaConfig.APISecret,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                RequestTimeoutMs = 45000,
            };

            var schemaRegistryConfig = new SchemaRegistryConfig
            {
                Url = kafkaConfig.SchemaRegistryUrl,
                BasicAuthCredentialsSource = AuthCredentialsSource.UserInfo,
                BasicAuthUserInfo = $"{_kafkaConfig.SchemaRegistryAPIKey}:{_kafkaConfig.SchemaRegistryAPISecret}"
            };

            SchemaRegistry = new CachedSchemaRegistryClient(schemaRegistryConfig);
            Producer = new ProducerBuilder<string, T>(config)
                .SetValueSerializer(Serializer)
                .Build();
            
        }

        public async Task<PersistenceStatus> ProduceAsync(T message, CancellationToken token)
        {
            try
            {
                var response = await Producer.ProduceAsync(PublishTopic,
                    new Message<string, T>
                    {
                        Key = PublishKey,
                        Value = message
                    }, token);

                return response.Status;
            }
            catch (Exception ex)
            {
                return PersistenceStatus.NotPersisted;
            }
        }

        public virtual void Dispose()
        {
            Producer?.Dispose();
        }
    }
}
