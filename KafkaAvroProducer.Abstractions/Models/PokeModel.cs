using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KafkaAvroProducer.Abstractions.Models
{
    public class PokeModel
    {
        //Class only used for generate Schemas and ISpecificRecord class
        public int Id { get; set; }
        public string Name { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public int BaseExperience { get; set; }
        public string Sprite { get; set; }
        public string Abilities { get; set; }
        public string Species { get; set; }
        public string Types { get; set; }
        public List<string> Stats { get; set; }
    }
}
