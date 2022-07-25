using Confluent.Kafka;
using LightMicroserviceModule.Domain.EventsBase;
using LightMicroserviceModule.Infrastructure.Kafka.Config;
using Microsoft.Extensions.Hosting;

namespace LightMicroserviceModule.Infrastructure.Kafka.Consumer;

public class KafkaConsumer<Tk, Tv> : IHostedService, IDisposable
{
    private readonly IConsumer<Tk, Tv> _consumer;

    private readonly IEventHandler<Tk, Tv> _handler;

    private readonly string _topic;
    
    public KafkaConsumer(KafkaConsumerConfig config, IConsumer<Tk, Tv> consumer, IEventHandler<Tk, Tv> handler)
    {
        _consumer = consumer;
        _topic = config.Topic;
        _handler = handler;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        Task.Run(() => ConsumeEvents(cancellationToken), cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;


    private async Task ConsumeEvents(CancellationToken token)
    {
        _consumer.Subscribe(_topic);

        while (!token.IsCancellationRequested)
        {
            var result = _consumer.Consume(TimeSpan.FromMilliseconds(1000));
            if (result == null)
                continue;

            Console.WriteLine($"From consumer: message value: {result.Message.Value}");
            await _handler.ProcessAsync(result.Message);
            _consumer.Commit(result);
        }
    }

    public void Dispose()
    {
        _consumer.Dispose();
    }
}