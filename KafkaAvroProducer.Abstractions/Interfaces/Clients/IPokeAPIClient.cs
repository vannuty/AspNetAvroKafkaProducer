using KafkaAvroProducer.Abstractions.Models;

namespace KafkaAvroProducer.Abstractions.Interfaces.Clients
{
    public interface IPokeAPIClient
    {
        Task<PokeListResultModel> GetFirstGeneration();
        Task<PokeResultModel> GetPokemonByURL(string url);
    }
}
