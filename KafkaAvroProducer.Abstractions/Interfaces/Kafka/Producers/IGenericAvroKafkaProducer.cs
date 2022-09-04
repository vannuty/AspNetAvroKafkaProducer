using Avro.Specific;
using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaAvroProducer.Abstractions.Interfaces.Kafka.Producers
{
    public interface IGenericAvroKafkaProducer<T> : IDisposable where T : ISpecificRecord, new()
    {
        Task<PersistenceStatus> ProduceAsync(T message, CancellationToken cancellationToken);
    }
}
