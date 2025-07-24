using AiRagProxy.Domain.Dtos.OpenAi;

namespace AiRagProxy.Api.Services.Interfaces;

public interface IOpenAiCommunicationService
{
    Task<OpenAiChatCompletionResponse> CreateChatCompletion(OpenAiChatCompletionRequest request);

    IAsyncEnumerable<ChatCompletionChunk> CreateChatCompletionStreaming(OpenAiChatCompletionRequest request,
        CancellationToken cancellationToken = default);
    
    Task<ModelsResponse?> GetModels(CancellationToken cancellationToken = default);
    
}