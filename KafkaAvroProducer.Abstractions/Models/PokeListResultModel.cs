using System.Text.Json.Serialization;


namespace KafkaAvroProducer.Abstractions.Models
{
    public class PokeList
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class PokeListResultModel
    {
        [JsonPropertyName("count")]
        public int Count { get; set; }

        [JsonPropertyName("next")]
        public string Next { get; set; }

        [JsonPropertyName("previous")]
        public object Previous { get; set; }

        [JsonPropertyName("results")]
        public List<PokeList> PokeList { get; set; }
    }
}
