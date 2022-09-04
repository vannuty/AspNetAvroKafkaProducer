using Microsoft.AspNetCore.Mvc;
using KafkaAvroProducer.Abstractions.Enums;
using KafkaAvroProducer.Abstractions.Interfaces.Clients;
using KafkaAvroProducer.Abstractions.Interfaces.Producers;
using KafkaAvroProducer.Abstractions.Models;
using KafkaAvroProducer.Producers.Config;

namespace KafkaAvroProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProducerController : ControllerBase
    {
        private readonly IExportInternalProducer _exportInternalProducer;
        private readonly KafkaConfig _kafkaConfig;
        private readonly IPokeAPIClient _pokeAPIClient;

        public ProducerController(IExportInternalProducer exportInternalProducer, KafkaConfig kafkaConfig, IPokeAPIClient pokeAPIClient)
        {
            _exportInternalProducer = exportInternalProducer;
            _kafkaConfig = kafkaConfig;
            _pokeAPIClient = pokeAPIClient;
        }

        [HttpGet("export-first-generation")]
        public async Task<IActionResult> ExportFirstGeneration()
        {
            await _exportInternalProducer.PublishMessageAsync(_kafkaConfig.Topics.InternalPokeExport, 
                _kafkaConfig.Topics.InternalPokeExport, new ExportRequestModel
                {
                    ProcessToStart = (int)ProcessToStartEnum.FirstGen
                });

            return Ok();
        }
    }
}
