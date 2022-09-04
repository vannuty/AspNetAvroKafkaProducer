using KafkaAvroProducer.Abstractions.Interfaces.Clients;
using KafkaAvroProducer.Abstractions.Interfaces.Kafka.Consumers;
using KafkaAvroProducer.Abstractions.Interfaces.Kafka.Producers;
using KafkaAvroProducer.Abstractions.Interfaces.Producers;
using KafkaAvroProducer.Abstractions.Models;
using KafkaAvroProducer.Clients;
using KafkaAvroProducer.Kafka.Consumers;
using KafkaAvroProducer.Kafka.Producers;
using KafkaAvroProducer.Producers.Config;
using KafkaAvroProducer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var kafkaConfig = builder.Configuration.GetSection("Kafka").Get<KafkaConfig>();
builder.Services.AddSingleton(kafkaConfig);

builder.Services.AddSingleton<IPokeAPIClient, PokeAPIClient>();
builder.Services.AddSingleton<IPokeExportClient, PokeExportClient>();

builder.Services.AddSingleton<IExportInternalProducer, ExportInternalProducer>();
builder.Services.AddSingleton<IExportInternalConsumer, ExportInternalConsume>();
builder.Services.AddSingleton<IGenericAvroKafkaProducer<PokeAvroModel>, PokeAvroKafkaProducer>();

builder.Services.AddSingleton<PokeAvroKafkaProducer>();

builder.Services.AddHttpClient("pokeapi", client =>
{
    client.BaseAddress = new Uri("https://pokeapi.co/api/v2/");
});

builder.Services.AddHostedService<PokeExportConsumerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
