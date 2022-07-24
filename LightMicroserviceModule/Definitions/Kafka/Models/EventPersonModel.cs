using System.Text.Json;

namespace LightMicroserviceModule.Definitions.Kafka.Models;

public class EventPersonModel
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public override string ToString()
    {
        return JsonSerializer.SerializeToDocument(this).ToString()!;
    }
}