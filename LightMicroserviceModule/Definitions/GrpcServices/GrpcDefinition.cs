using LightMicroserviceModule.Definitions.Base;

namespace LightMicroserviceModule.Definitions.GrpcServices;

public class GrpcDefinition : AppDefinition
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddGrpc();
    }

    public override void ConfigureApplication(WebApplication app, IWebHostEnvironment env)
    {
        app.UseRouting();
    }
}