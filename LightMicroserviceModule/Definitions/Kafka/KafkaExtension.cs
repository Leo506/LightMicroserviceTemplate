using Confluent.Kafka;
using DnsClient.Protocol;
using LightMicroserviceModule.Definitions.Kafka.Config;
using LightMicroserviceModule.Definitions.Kafka.Producer;
using LightMicroserviceModule.EventsBase;

namespace LightMicroserviceModule.Definitions.Kafka;

public static class KafkaExtension
{
    public static IServiceCollection AddKafkaProducer<Tk, Tv>(this IServiceCollection services,
        KafkaConfig config, ISerializer<Tv> serializer)
    {
        services.AddSingleton(config);

        services.AddTransient<IProducer<Tk, Tv>>(provider =>
            new ProducerBuilder<Tk, Tv>(config.ProducerConfig).SetValueSerializer(serializer).Build());

        services.AddSingleton<IEventProducer<Tk, Tv>, KafkaProducer<Tk, Tv>>();

        return services;
    }
}