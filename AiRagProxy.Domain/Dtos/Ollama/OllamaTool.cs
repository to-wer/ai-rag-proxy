using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.Ollama;

public class OllamaTool
{
    [JsonPropertyName("type")] public required string Type { get; set; }
    [JsonPropertyName("function")] public OllamaFunction? Function { get; set; }
}

