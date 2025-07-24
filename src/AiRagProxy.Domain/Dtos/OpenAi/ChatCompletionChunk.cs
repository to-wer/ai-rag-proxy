using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.OpenAi;

public class ChatCompletionChunk
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("object")] public string Object { get; set; } = "chat.completion.chunk";
    [JsonPropertyName("created")] public long Created { get; set; }
    [JsonPropertyName("model")] public string Model { get; set; }
    [JsonPropertyName("choices")] public List<Choice> Choices { get; set; }

    public class Choice
    {
        [JsonPropertyName("index")] public int Index { get; set; }
        [JsonPropertyName("delta")] public Message Delta { get; set; }
        [JsonPropertyName("finish_reason")] public string? FinishReason { get; set; } = null;
    }

    public class Message
    {
        [JsonPropertyName("content")] public string Content { get; set; } = string.Empty;
    }
}