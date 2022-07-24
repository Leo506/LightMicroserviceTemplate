using Calabonga.OperationResults;
using Confluent.Kafka;
using LightMicroserviceModule.Definitions.Kafka.Models;
using LightMicroserviceModule.EventsBase;

namespace LightMicroserviceModule.Definitions.Kafka.Consumer.Handlers;

public class TestHandler : IEventHandler<Null, EventPersonModel>
{ public void Process(Message<Null, EventPersonModel> message)
    {
        Console.WriteLine($"Got message: {message.Value.ToString()}");
    }

    public Task<OperationResult<bool>> ProcessAsync(Message<Null, EventPersonModel> message)
    {
        Console.WriteLine($"Got message: {message.Value.ToString()}");
        return Task.FromResult(new OperationResult<bool>() { Result = true });
    }
}