using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.Ollama;

public class OllamaChatCompletionRequest
{
    [JsonPropertyName("model")] public required string Model { get; set; }

    [JsonPropertyName("messages")] public required List<OllamaChatMessage> Messages { get; set; }
    [JsonPropertyName("stream")] public bool Stream { get; set; } = false;
}