using KafkaAvroProducer.Abstractions.Models;

namespace KafkaAvroProducer.Abstractions.Interfaces.Producers
{
    public interface IExportInternalProducer : IJsonKafkaProducer<ExportRequestModel>
    {
    }
}
