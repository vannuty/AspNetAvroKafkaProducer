using System.Reflection;
using System.Text.Json.Serialization;

namespace KafkaAvroProducer.Abstractions.Models
{
    public class PokeResultModel
    {
        [JsonPropertyName("abilities")]
        public List<Ability> Abilities { get; set; }

        [JsonPropertyName("base_experience")]
        public int BaseExperience { get; set; }

        [JsonPropertyName("height")]
        public int Height { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("species")]
        public Species Species { get; set; }

        [JsonPropertyName("sprites")]
        public Sprites Sprites { get; set; }

        [JsonPropertyName("stats")]
        public List<Stat> Stats { get; set; }

        [JsonPropertyName("types")]
        public List<Type> Types { get; set; }

        [JsonPropertyName("weight")]
        public int Weight { get; set; }

        public PokeAvroModel ToPokeAvroModel()
        {
            var pokeAvroModel = new PokeAvroModel
            {
                Id = this.Id,
                Name = this.Name,
                Height = this.Height,
                Weight = this.Weight,
                BaseExperience = this.BaseExperience,
                Sprite = this.Sprites.FrontDefault,
                Abilities = String.Join(", ", this.Abilities.Select(x => x.AbilityInfo.Name)),
                Species = this.Species.Name,
                Types = String.Join(", ", this.Types.Select(x => x.TypeInfo.Name)),
                Stats = new List<string>()
            };

            foreach (var item in this.Stats)
            {
                pokeAvroModel.Stats.Add($"{item.StatInfo.Name}: {item.BaseStat}");
            }

            return pokeAvroModel;
        }
    }
    public class Ability
    {
        [JsonPropertyName("ability")]
        public AbilityInfo AbilityInfo { get; set; }

        [JsonPropertyName("is_hidden")]
        public bool IsHidden { get; set; }

        [JsonPropertyName("slot")]
        public int Slot { get; set; }
    }

    public class AbilityInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Species
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Sprites
    {
        [JsonPropertyName("back_default")]
        public string BackDefault { get; set; }

        [JsonPropertyName("back_female")]
        public object BackFemale { get; set; }

        [JsonPropertyName("back_shiny")]
        public string BackShiny { get; set; }

        [JsonPropertyName("back_shiny_female")]
        public object BackShinyFemale { get; set; }

        [JsonPropertyName("front_default")]
        public string FrontDefault { get; set; }

        [JsonPropertyName("front_female")]
        public object FrontFemale { get; set; }

        [JsonPropertyName("front_shiny")]
        public string FrontShiny { get; set; }

        [JsonPropertyName("front_shiny_female")]
        public object FrontShinyFemale { get; set; }
    }

    public class Stat
    {
        [JsonPropertyName("base_stat")]
        public int BaseStat { get; set; }

        [JsonPropertyName("effort")]
        public int Effort { get; set; }

        [JsonPropertyName("stat")]
        public StatInfo StatInfo { get; set; }
    }

    public class StatInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }

    public class Type
    {
        [JsonPropertyName("slot")]
        public int Slot { get; set; }

        [JsonPropertyName("type")]
        public TypeInfo TypeInfo { get; set; }
    }

    public class TypeInfo
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("url")]
        public string Url { get; set; }
    }
}
