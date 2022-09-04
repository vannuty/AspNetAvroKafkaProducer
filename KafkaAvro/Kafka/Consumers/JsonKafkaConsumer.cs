using Confluent.Kafka;
using KafkaAvroProducer.Abstractions.Interfaces.Kafka.Consumers;
using KafkaAvroProducer.Producers.Config;
using System.Text.Json;

namespace KafkaAvroProducer.Kafka.Consumers
{
    public abstract class JsonKafkaConsumer<T> : IJsonKafkaConsumer<T>
    {
        private readonly KafkaConfig _kafkaConfig;

        protected JsonKafkaConsumer(KafkaConfig kafkaConfig)
        {
            _kafkaConfig = kafkaConfig;
        }

        protected abstract string ConsumeTopic { get; }

        public async Task ConsumeAsync(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _kafkaConfig.Server,
                SaslUsername = _kafkaConfig.APIKey,
                SaslPassword = _kafkaConfig.APISecret,
                SaslMechanism = SaslMechanism.Plain,
                SecurityProtocol = SecurityProtocol.SaslSsl,
                GroupId = ConsumeTopic,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = true
            };

            using var consumer = new ConsumerBuilder<string, string>(config).Build();

            consumer.Subscribe(ConsumeTopic);

            while (!cancellationToken.IsCancellationRequested)
            {
                var kafkaMessage = string.Empty;

                var consumeResult = consumer.Consume(cancellationToken);

                kafkaMessage = consumeResult.Message?.Value;

                var message = JsonSerializer.Deserialize<T>(kafkaMessage);

                await DoWork(message, cancellationToken);
            }

            consumer.Close();
        }

        public abstract Task DoWork(T message, CancellationToken cancellationToken);
    }
}

