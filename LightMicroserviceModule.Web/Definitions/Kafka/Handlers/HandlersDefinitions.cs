using Confluent.Kafka;
using GrpcServices;
using LightMicroserviceModule.Definitions.Base;
using LightMicroserviceModule.Definitions.Kafka.Models;
using LightMicroserviceModule.Domain.EventsBase;

namespace LightMicroserviceModule.Definitions.Kafka.Handlers;

public class HandlersDefinitions : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IEventHandler<Null, Request>, RequestHadler>();
        services.AddTransient<IEventHandler<Null, EventPersonModel>, TestHandler>();
    }
}