using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.OpenAi;

namespace AiRagProxy.Api.Services;

public class OllamaChatCompletionProvider : IChatCompletionProvider
{
    public Task<OpenAiChatCompletionResponse> CreateChatCompletion(OpenAiChatCompletionRequest request)
    {
        throw new NotImplementedException();
    }

    public IAsyncEnumerable<ChatCompletionChunk> CreateChatCompletionStreaming(OpenAiChatCompletionRequest request,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task<ModelsResponse?> GetModels(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}