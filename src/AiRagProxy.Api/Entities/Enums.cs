
namespace AiRagProxy.Api.Entities;

public enum ProviderType
{
    OpenAI,
    AzureOpenAI,
    Ollama,
    Anthropic
}

public enum AuthenticationType
{
    ApiKey,
    BearerToken,
    AzureAad
}
