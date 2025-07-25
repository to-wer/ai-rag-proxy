using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Enums;

namespace AiRagProxy.Api.Services;

public class ChatCompletionProviderFactory(IServiceProvider serviceProvider, IConfiguration configuration)
    : IChatCompletionProviderFactory
{
    private readonly IConfiguration _configuration = configuration;

    public IChatCompletionProvider GetProvider(ProviderType providerType)
    {
        return providerType switch
        {
            ProviderType.OpenAi => serviceProvider.GetRequiredService<OpenAiChatCompletionProvider>(),
            ProviderType.Ollama => serviceProvider.GetRequiredService<OllamaChatCompletionProvider>(),
            _ => throw new InvalidOperationException("Unknown provider")
        };
    }
}