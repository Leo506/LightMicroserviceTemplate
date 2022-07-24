using Confluent.Kafka;
using DnsClient.Protocol;
using LightMicroserviceModule.Definitions.Kafka.Config;
using LightMicroserviceModule.Definitions.Kafka.Consumer;
using LightMicroserviceModule.Definitions.Kafka.Consumer.Handlers;
using LightMicroserviceModule.Definitions.Kafka.Producer;
using LightMicroserviceModule.EventsBase;

namespace LightMicroserviceModule.Definitions.Kafka;

public static class KafkaExtension
{
    public static IServiceCollection AddKafkaProducer<Tk, Tv>(this IServiceCollection services,
        KafkaProducerConfig producerConfig, ISerializer<Tv> serializer)
    {
        services.AddSingleton(producerConfig);

        services.AddTransient<IProducer<Tk, Tv>>(provider =>
            new ProducerBuilder<Tk, Tv>(producerConfig.ProducerConfig).SetValueSerializer(serializer).Build());

        services.AddSingleton<IEventProducer<Tk, Tv>, KafkaProducer<Tk, Tv>>();

        return services;
    }


    public static IServiceCollection AddKafkaConsumer<Tk, Tv>(this IServiceCollection services,
        KafkaConsumerConfig consumerConfig, IDeserializer<Tv> deserializer, IEventHandler<Tk, Tv> handler)
    {
        services.AddSingleton(consumerConfig);

        services.AddTransient<IConsumer<Tk, Tv>>(provider =>
            new ConsumerBuilder<Tk, Tv>(consumerConfig.ConsumerConfig).SetValueDeserializer(deserializer).Build());

        services.AddSingleton(handler);
        
        services.AddHostedService<KafkaConsumer<Tk, Tv>>();

        return services;
    }
}