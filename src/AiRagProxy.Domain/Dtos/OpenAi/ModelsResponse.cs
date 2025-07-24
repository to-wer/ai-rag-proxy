using System.Text.Json.Serialization;

namespace AiRagProxy.Domain.Dtos.OpenAi;

public class ModelsResponse
{
    [JsonPropertyName("object")] public string Object { get; set; } = "list";
    [JsonPropertyName("data")] public List<Model> Data { get; set; } = [];
}