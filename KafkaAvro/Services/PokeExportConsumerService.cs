using KafkaAvroProducer.Abstractions.Interfaces.Kafka.Consumers;
using KafkaAvroProducer.Abstractions.Models;

namespace KafkaAvroProducer.Services
{
    public class PokeExportConsumerService : IHostedService
    {
        private readonly IExportInternalConsumer _consumer;

        public PokeExportConsumerService(IExportInternalConsumer consumer)
        {
            _consumer = consumer;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("PokeExportConsumerService Started");

            Task.Run(() => ExecuteAsync(cancellationToken), cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("PokeExportConsumerService Stopped");

            return Task.CompletedTask;
        }

        public async Task ExecuteAsync(CancellationToken cancellationToken) => _consumer.ConsumeAsync(cancellationToken);

    }
}
