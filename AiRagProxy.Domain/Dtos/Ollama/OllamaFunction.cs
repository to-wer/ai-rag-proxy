using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.Ollama;

public class OllamaFunction
{
    [JsonPropertyName("name")] public required string Name { get; set; }
    [JsonPropertyName("description")] public string? Description { get; set; }
    [JsonPropertyName("parameters")] public OllamaFunctionParameters? Parameters { get; set; }
}

