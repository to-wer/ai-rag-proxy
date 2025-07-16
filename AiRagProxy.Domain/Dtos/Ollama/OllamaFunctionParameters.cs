using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.Ollama;

public class OllamaFunctionParameters
{
    [JsonPropertyName("type")] public required string Type { get; set; }
    [JsonPropertyName("properties")] public Dictionary<string, OllamaPropertyDefinition>? Properties { get; set; }
    [JsonPropertyName("required")] public List<string>? Required { get; set; }
}

