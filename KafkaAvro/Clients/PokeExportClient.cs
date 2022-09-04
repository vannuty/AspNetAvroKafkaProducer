using Confluent.Kafka;
using KafkaAvroProducer.Abstractions.Interfaces.Clients;
using KafkaAvroProducer.Kafka.Producers;
using System.Text.Json;

namespace KafkaAvroProducer.Clients
{
    public class PokeExportClient : IPokeExportClient
    {
        private readonly IPokeAPIClient _pokeAPIClient;
        private readonly PokeAvroKafkaProducer _producer;

        public PokeExportClient(IPokeAPIClient pokeAPIClient, PokeAvroKafkaProducer producer)
        {
            _pokeAPIClient = pokeAPIClient;
            _producer = producer;
        }

        public async Task ExportPokeToKafkaAsync(CancellationToken cancellationToken)
        {
             var result = await _pokeAPIClient.GetFirstGeneration();

            foreach (var item in result.PokeList)
            {
                var pokeResult = await _pokeAPIClient.GetPokemonByURL(item.Url);
                var pokeAvro = pokeResult.ToPokeAvroModel();

                await _producer.ProduceAsync(pokeAvro, cancellationToken);
            }
        }
    }
}
