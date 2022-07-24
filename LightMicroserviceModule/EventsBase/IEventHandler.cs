using Confluent.Kafka;

namespace LightMicroserviceModule.EventsBase;

public interface IEventHandler<Tk, Tv>
{
    void Process(Message<Tk, Tv> message);
}