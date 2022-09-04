using KafkaAvroProducer.Abstractions.Interfaces.Clients;
using KafkaAvroProducer.Abstractions.Interfaces.Kafka.Consumers;
using KafkaAvroProducer.Abstractions.Models;
using KafkaAvroProducer.Clients;
using KafkaAvroProducer.Producers.Config;

namespace KafkaAvroProducer.Kafka.Consumers
{
    public class ExportInternalConsume : JsonKafkaConsumer<ExportRequestModel>, IExportInternalConsumer
    {
        private readonly KafkaConfig _kafkaConfig;
        private readonly IPokeExportClient _pokeExportClient;

        public ExportInternalConsume(KafkaConfig kafkaConfig, IPokeExportClient pokeExportClient) : base(kafkaConfig)
        {
            _kafkaConfig = kafkaConfig;
            _pokeExportClient = pokeExportClient;
        }

        protected override string ConsumeTopic => _kafkaConfig.Topics.InternalPokeExport;

        public override async Task DoWork(ExportRequestModel message, CancellationToken cancellationToken)
        {
            _pokeExportClient.ExportPokeToKafkaAsync(cancellationToken);
        }
    }
}
