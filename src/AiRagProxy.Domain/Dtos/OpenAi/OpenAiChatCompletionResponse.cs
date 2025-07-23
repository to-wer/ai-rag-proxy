using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.OpenAi;

public class OpenAiChatCompletionResponse
{
    [JsonPropertyName("id")] public string Id { get; set; } = string.Empty;

    [JsonPropertyName("object")] public string Object { get; set; } = string.Empty;

    [JsonPropertyName("created")] public long Created { get; set; }

    [JsonPropertyName("model")] public string Model { get; set; } = string.Empty;

    [JsonPropertyName("choices")] public List<Choice> Choices { get; set; } = new();

    [JsonPropertyName("usage")] public OpenAiUsage Usage { get; set; } = new();

    [JsonPropertyName("service_tier")] public string ServiceTier { get; set; } = string.Empty;

    public class Choice
    {
        [JsonPropertyName("index")] public int Index { get; set; }

        [JsonPropertyName("message")] public Message Message { get; set; } = new();

        [JsonPropertyName("logprobs")] public object? LogProbs { get; set; }

        [JsonPropertyName("finish_reason")] public string FinishReason { get; set; } = string.Empty;
    }

    public class Message
    {
        [JsonPropertyName("role")] public string Role { get; set; } = string.Empty;

        [JsonPropertyName("content")] public string Content { get; set; } = string.Empty;

        [JsonPropertyName("refusal")] public string? Refusal { get; set; }

        [JsonPropertyName("annotations")] public List<object> Annotations { get; set; } = new();
    }

    public class OpenAiUsage
    {
        [JsonPropertyName("prompt_tokens")] public int PromptTokens { get; set; }

        [JsonPropertyName("completion_tokens")]
        public int CompletionTokens { get; set; }

        [JsonPropertyName("total_tokens")] public int TotalTokens { get; set; }

        [JsonPropertyName("prompt_tokens_details")]
        public PromptTokensDetails PromptTokensDetails { get; set; } = new();

        [JsonPropertyName("completion_tokens_details")]
        public CompletionTokensDetails CompletionTokensDetails { get; set; } = new();
    }

    public class PromptTokensDetails
    {
        [JsonPropertyName("cached_tokens")] public int CachedTokens { get; set; }

        [JsonPropertyName("audio_tokens")] public int AudioTokens { get; set; }
    }

    public class CompletionTokensDetails
    {
        [JsonPropertyName("reasoning_tokens")] public int ReasoningTokens { get; set; }

        [JsonPropertyName("audio_tokens")] public int AudioTokens { get; set; }

        [JsonPropertyName("accepted_prediction_tokens")]
        public int AcceptedPredictionTokens { get; set; }

        [JsonPropertyName("rejected_prediction_tokens")]
        public int RejectedPredictionTokens { get; set; }
    }
}