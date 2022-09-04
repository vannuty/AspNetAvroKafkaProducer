namespace KafkaAvroProducer.Producers.Config
{
    public class KafkaConfig
    {
        public string APIKey { get; set; }
        public string APISecret { get; set; }
        public string Server { get; set; }
        public string SchemaRegistryUrl { get; set; }
        public string SchemaRegistryAPIKey { get; set; }
        public string SchemaRegistryAPISecret { get; set; }
        

        public Topics Topics { get; set; }
    }

    public class Topics
    {
        public string InternalPokeExport { get; set; }
        public string PokeExportAvro { get; set; }
    }
}
