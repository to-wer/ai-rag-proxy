using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.OpenAi;

public class OpenAiChatCompletionRequest
{
    [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;
    [JsonPropertyName("messages")] public List<OpenAiChatMessage> Messages { get; set; } = new();
    [JsonPropertyName("stream")] public bool Stream { get; set; }
    // [JsonPropertyName("max_tokens")] public int? MaxTokens { get; set; }
    [JsonPropertyName("temperature")] public double Temperature { get; set; } = 1.0;
}