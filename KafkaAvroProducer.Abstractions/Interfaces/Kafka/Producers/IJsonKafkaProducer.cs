using Confluent.Kafka;

namespace KafkaAvroProducer.Abstractions.Interfaces.Producers
{
    public interface IJsonKafkaProducer<T>
    {
        Task<PersistenceStatus> PublishMessageAsync(string topic, string key, T message);
    }
}
