using Confluent.Kafka;
using Confluent.Kafka.SyncOverAsync;
using Confluent.SchemaRegistry;
using Google.Protobuf;
using LightMicroserviceModule.Domain.EventsBase;
using LightMicroserviceModule.EventsBase;
using LightMicroserviceModule.Infrastructure.Kafka.Config;
using LightMicroserviceModule.Infrastructure.Kafka.Consumer;
using LightMicroserviceModule.Infrastructure.Kafka.Producer;
using LightMicroserviceModule.Infrastructure.Kafka.Protobuf;

namespace LightMicroserviceModule.Definitions.Kafka;

public static class KafkaExtension
{
    public static IServiceCollection AddKafkaProducer<TKey, TValue>(this IServiceCollection services,
        KafkaProducerConfig producerConfig, ISerializer<TValue> serializer)
    {
        services.AddSingleton(producerConfig);  // TODO we don't need config in DI

        services.AddTransient<IProducer<TKey, TValue>>(provider =>
            new ProducerBuilder<TKey, TValue>(producerConfig.ProducerConfig).SetValueSerializer(serializer).Build());

        services.AddSingleton<IEventProducer<TKey, TValue>, KafkaProducer<TKey, TValue>>();

        return services;
    }

    public static IServiceCollection AddKafkaProducer<TKey, TValue>(this IServiceCollection services,
        KafkaProducerConfig producerConfig) where TValue : IMessage<TValue>, new()
    {
        services.AddSingleton(producerConfig);  // TODO we don't need config in DI

        services.AddTransient<IProducer<TKey, TValue>>(provider =>
            new ProducerBuilder<TKey, TValue>(producerConfig.ProducerConfig)
                .SetValueSerializer(new ProtobufSerializer<TValue>()).Build());
        
        services.AddSingleton<IEventProducer<TKey, TValue>, KafkaProducer<TKey, TValue>>();

        return services;
    }


    public static IServiceCollection AddKafkaConsumer<Tk, Tv>(this IServiceCollection services,
        KafkaConsumerConfig consumerConfig, IDeserializer<Tv> deserializer, IEventHandler<Tk, Tv> handler)
    {
        services.AddSingleton(consumerConfig);  // TODO we don't need config in DI

        services.AddTransient<IConsumer<Tk, Tv>>(provider =>
            new ConsumerBuilder<Tk, Tv>(consumerConfig.ConsumerConfig).SetValueDeserializer(deserializer).Build());

        services.AddSingleton(handler);
        
        services.AddHostedService<KafkaConsumer<Tk, Tv>>();

        return services;
    }


    public static IServiceCollection AddKafkaConsumer<TKey, TValue>(this IServiceCollection services,
        KafkaConsumerConfig consumerConfig, IEventHandler<TKey, TValue> handler) where TValue : class, IMessage<TValue>, new()
    {
        services.AddSingleton(consumerConfig);  // TODO we don't need config in DI

        services.AddTransient<IConsumer<TKey, TValue>>(provider =>
            new ConsumerBuilder<TKey, TValue>(consumerConfig.ConsumerConfig)
                .SetValueDeserializer(new ProtobufDeserializer<TValue>()).Build());
        
        services.AddSingleton(handler);
        
        services.AddHostedService<KafkaConsumer<TKey, TValue>>();

        return services;
    }
}