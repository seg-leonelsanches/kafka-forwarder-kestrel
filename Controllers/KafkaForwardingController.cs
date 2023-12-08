using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace KafkaForwarderKestrel.Controllers;

[ApiController]
[Route("")]
public class KafkaForwardingController : ControllerBase
{

    private readonly ILogger<KafkaForwardingController> _logger;
    private readonly IProducer<string, string> _producer;
    private readonly string _topic;

    public KafkaForwardingController(
        ILogger<KafkaForwardingController> logger, 
        IProducer<string, string> producer,
        IConfiguration configuration)
    {
        _logger = logger;
        _producer = producer;
        _topic = configuration.GetValue<string>("Kafka:Topic");
    }

    // This is a synchronous callback.
    public static void handler(DeliveryReport<Null, string> report)
    {
        Console.WriteLine($"Delivered '{report.Value}' to '{report.TopicPartitionOffset}'");
    }

    [HttpGet(Name = "Forwarder")]
    public async Task<string> Get()
    {
        var result = await _producer.ProduceAsync(_topic, new Message<string, string> { Key="Segment", Value="a log message" });
        _logger.LogInformation($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
        return "Ok";
        // To use the synchronous version, uncomment the following lines:
        /* _producer.Produce("drivetime", new Message<Null, string> { Value = "hello world" }, handler);
        return "Ok"; */
    }

    [HttpPost(Name = "Forwarder")]
    public async Task<string> Post([FromBody] IDictionary<string, string> data)
    {
        var result = await _producer.ProduceAsync(_topic, new Message<string, string> { Key="Segment", Value=JsonConvert.SerializeObject(data) });
        _logger.LogInformation($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
        return "Ok";
    }
}
