using KafkaAvroProducer.Abstractions.Models;
using SolTechnology.Avro;
using System.IO;

internal class Program
{
    private static void Main(string[] args)
    {
        //Only need to build this project to Generate the asvc file and C# Avro Class
        var schema = AvroConvert.GenerateSchema(typeof(PokeModel));
        schema = schema.Replace("PokeModel", "PokeAvroModel");
        var filename = "PokeAvroModel.asvc";
        File.WriteAllText(filename, schema);
        string newPath = Path.Combine(Directory.GetCurrentDirectory(), @"..\KafkaAvroProducer.Abstractions\Models\Avro\Schemas\");
        File.Copy(filename, newPath + filename, true);
    }
}