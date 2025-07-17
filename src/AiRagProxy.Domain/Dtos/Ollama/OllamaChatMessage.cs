using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.Ollama;

public class OllamaChatMessage
{
    [JsonPropertyName("role")] public required string Role { get; set; }

    [JsonPropertyName("content")] public required string Content { get; set; }
}