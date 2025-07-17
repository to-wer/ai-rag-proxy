using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.Ollama;

public class OllamaPropertyDefinition
{
    [JsonPropertyName("type")] public required string Type { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
}

