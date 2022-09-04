using KafkaAvroProducer.Abstractions.Models;
using KafkaAvroProducer.Producers.Config;

namespace KafkaAvroProducer.Kafka.Producers
{
    public class PokeAvroKafkaProducer : GenericAvroKafkaProducer<PokeAvroModel>
    {
        private readonly KafkaConfig _kafkaConfig;

        public PokeAvroKafkaProducer(KafkaConfig kafkaConfig) : base(kafkaConfig)
        {
            _kafkaConfig = kafkaConfig;
        }

        protected override string PublishTopic => _kafkaConfig.Topics.PokeExportAvro;

        protected override string PublishKey => _kafkaConfig.Topics.PokeExportAvro;
    }
}
