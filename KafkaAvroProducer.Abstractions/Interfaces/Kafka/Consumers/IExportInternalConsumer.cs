using KafkaAvroProducer.Abstractions.Models;


namespace KafkaAvroProducer.Abstractions.Interfaces.Kafka.Consumers
{
    public interface IExportInternalConsumer : IJsonKafkaConsumer<ExportRequestModel>
    {
    }
}
