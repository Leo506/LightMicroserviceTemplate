using Confluent.Kafka;

namespace LightMicroserviceModule.Infrastructure.Kafka.Config;

public class KafkaConsumerConfig : KafkaConfig
{
    public string Group { get; set; } = null!;

    public ConsumerConfig ConsumerConfig =>
        new ConsumerConfig()
        {
            BootstrapServers = KafkaHost,
            GroupId = Group,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };
}