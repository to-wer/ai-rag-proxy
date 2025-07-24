using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.OpenAi;

public class Model
{
    [JsonPropertyName("id")] public required string Id { get; set; }
    [JsonPropertyName("object")] public required string Object { get; set; }
    [JsonPropertyName("created")]  public long Created { get; set; }
    [JsonPropertyName("owned_by")] public string? OwnedBy { get; set; }
}