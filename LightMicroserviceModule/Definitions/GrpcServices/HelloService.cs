using Grpc.Core;
using GrpcServices;

namespace LightMicroserviceModule.Definitions.GrpcServices;

public class HelloService : SayHelloService.SayHelloServiceBase
{
    public override Task<Response> SayHello(Request request, ServerCallContext context)
    {
        return Task.FromResult(new Response() { Answer = "Hello, " + request.Name });
    }
}