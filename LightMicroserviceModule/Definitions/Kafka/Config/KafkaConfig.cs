using System.Net;
using Confluent.Kafka;

namespace LightMicroserviceModule.Definitions.Kafka.Config;

public class KafkaConfig
{
    public string Topic { get; set; } = null!;
    public string KafkaHost { get; set; } = null!;
    
    public ProducerConfig ProducerConfig =>
        new ProducerConfig()
        {
            BootstrapServers = KafkaHost,
            ClientId = Dns.GetHostName()
        };
}