using Calabonga.OperationResults;
using Confluent.Kafka;

namespace LightMicroserviceModule.EventsBase;

public interface IEventHandler<Tk, Tv>
{
    void Process(Message<Tk, Tv> message);

    Task<OperationResult<bool>> ProcessAsync(Message<Tk, Tv> message);
}