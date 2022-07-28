using Calabonga.OperationResults;
using Confluent.Kafka;
using GrpcServices;
using LightMicroserviceModule.Domain.EventsBase;

namespace LightMicroserviceModule.Definitions.Kafka.Handlers;

public class RequestHadler : IEventHandler<Null, Request>
{
    public void Process(Message<Null, Request> message)
    {
        Console.WriteLine(message.Value);
    }

    public Task<OperationResult<bool>> ProcessAsync(Message<Null, Request> message)
    {
        Console.WriteLine(message.Value);

        return Task.FromResult<OperationResult<bool>>(new OperationResult<bool>() { Result = true });
    }
}