using Avro.Specific;
using Confluent.Kafka;
using Confluent.SchemaRegistry.Serdes;
using KafkaAvroProducer.Abstractions.Interfaces.Kafka.Producers;
using KafkaAvroProducer.Producers.Config;

namespace KafkaAvroProducer.Kafka.Producers
{
    public abstract class GenericAvroKafkaProducer<T> : GenericKafkaProducer<T>, IGenericAvroKafkaProducer<T> where T : ISpecificRecord, new()
    {
        protected override IAsyncSerializer<T> Serializer => new AvroSerializer<T>(SchemaRegistry);
        protected GenericAvroKafkaProducer(KafkaConfig kafkaConfig) : base(kafkaConfig)
        {
        }
    }
}
