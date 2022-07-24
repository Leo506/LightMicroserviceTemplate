using Calabonga.OperationResults;

namespace LightMicroserviceModule.EventsBase;

public interface IEventProducer<Tk, Tv>
{
    Task<OperationResult<bool>> ProduceAsync(Tk key, Tv value);
}