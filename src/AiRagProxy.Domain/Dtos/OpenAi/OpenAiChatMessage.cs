namespace AiRagProxy.Domain.Dtos.OpenAi;

public class OpenAiChatMessage
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}