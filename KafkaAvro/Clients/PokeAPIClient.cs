using KafkaAvroProducer.Abstractions.Interfaces.Clients;
using KafkaAvroProducer.Abstractions.Models;
using System.Runtime.InteropServices;

namespace KafkaAvroProducer.Clients
{
    public class PokeAPIClient : IPokeAPIClient
    {

        private readonly IHttpClientFactory _httpClientFactory;

        public PokeAPIClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<PokeListResultModel> GetFirstGeneration()
        {
            var client = _httpClientFactory.CreateClient("pokeapi");

            return await client.GetFromJsonAsync<PokeListResultModel>("pokemon/?limit=151");
        }

        public async Task<PokeResultModel> GetPokemonByURL(string url)
        {
            var client = _httpClientFactory.CreateClient();

            return await client.GetFromJsonAsync<PokeResultModel>(url);
        }
    }
}
