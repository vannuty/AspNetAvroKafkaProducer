using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaAvroProducer.Abstractions.Interfaces.Kafka.Consumers
{
    public interface IJsonKafkaConsumer<T>
    {
        Task ConsumeAsync(CancellationToken cancellationToken);
    }
}
