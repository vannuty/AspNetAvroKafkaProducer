using KafkaAvroProducer.Abstractions.Interfaces.Producers;
using KafkaAvroProducer.Abstractions.Models;
using KafkaAvroProducer.Producers.Config;

namespace KafkaAvroProducer.Kafka.Producers
{
    public class ExportInternalProducer : JsonKafkaProducer<ExportRequestModel>, IExportInternalProducer
    {
        public ExportInternalProducer(KafkaConfig kafkaConfig) : base(kafkaConfig)
        {
        }
    }
}
