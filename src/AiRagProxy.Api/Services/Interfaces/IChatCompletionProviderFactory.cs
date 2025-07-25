using AiRagProxy.Domain.Enums;

namespace AiRagProxy.Api.Services.Interfaces;

public interface IChatCompletionProviderFactory
{
    IChatCompletionProvider GetProvider(ProviderType providerType);
}