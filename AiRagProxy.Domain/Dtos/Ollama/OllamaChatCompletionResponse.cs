using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.Ollama;

public class OllamaChatCompletionResponse
{
    [JsonPropertyName("model")] public required string Model { get; set; }
    [JsonPropertyName("created_at")] public required DateTime CreatedAt { get; set; }
    [JsonPropertyName("message")] public OllamaChatMessage? Message { get; set; }
    [JsonPropertyName("done")] public bool Done { get; set; } = false;
    [JsonPropertyName("total_duration")] public int TotalDuration { get; set; } = 0;
    [JsonPropertyName("load_duration")] public int LoadDuration { get; set; } = 0;

    [JsonPropertyName("prompt_eval_count")]
    public int PromptEvalCount { get; set; } = 0;

    [JsonPropertyName("prompt_eval_duration")]
    public int PromptEvalDuration { get; set; } = 0;

    [JsonPropertyName("eval_count")] public int EvalCount { get; set; } = 0;
    [JsonPropertyName("eval_duration")] public int EvalDuration { get; set; } = 0;
}