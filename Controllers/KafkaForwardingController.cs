using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;

namespace KafkaForwarderKestrel.Controllers;

[ApiController]
[Route("")]
public class KafkaForwardingController : ControllerBase
{

    private readonly ILogger<KafkaForwardingController> _logger;
    private readonly IProducer<string, string> _producer;

    public KafkaForwardingController(ILogger<KafkaForwardingController> logger, IProducer<string, string> producer)
    {
        _logger = logger;
        _producer = producer;
    }

    public static void handler(DeliveryReport<Null, string> report)
    {
        Console.WriteLine($"Delivered '{report.Value}' to '{report.TopicPartitionOffset}'");
    }

    [HttpGet(Name = "Forwarder")]
    public async Task<string> Get()
    {
        var result = await _producer.ProduceAsync("drivetime", new Message<string, string> { Key="123", Value="a log message" });
        _logger.LogInformation($"Delivered '{result.Value}' to '{result.TopicPartitionOffset}'");
        return "Ok";
        // To use the synchronous version, uncomment the following lines:
        /* _producer.Produce("drivetime", new Message<Null, string> { Value = "hello world" }, handler);
        return "Ok"; */
    }
}
