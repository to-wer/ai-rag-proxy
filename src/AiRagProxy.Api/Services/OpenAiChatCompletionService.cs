using System.Runtime.CompilerServices;
using System.Text.Json;
using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.OpenAi;

namespace AiRagProxy.Api.Services;

public class OpenAiChatCompletionService(
    HttpClient httpClient,
    ILogger<OpenAiChatCompletionService> logger)
    : IOpenAiChatCompletionService
{
    public async Task<OpenAiChatCompletionResponse> CreateChatCompletion(OpenAiChatCompletionRequest request)
    {
        try
        {
            logger.LogInformation("Sending chat completion request to OpenAI API");

            var response = await httpClient.PostAsJsonAsync("v1/chat/completions", request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                logger.LogError("OpenAI API request failed with status code {StatusCode}: {ErrorContent}",
                    response.StatusCode, errorContent);
                throw new HttpRequestException(
                    $"OpenAI API request failed with status code {response.StatusCode}: {errorContent}");
            }

            var completionResponse = await response.Content.ReadFromJsonAsync<OpenAiChatCompletionResponse>();

            if (completionResponse == null)
            {
                throw new JsonException("Failed to deserialize OpenAI API response");
            }

            return completionResponse;
        }
        catch (HttpRequestException ex)
        {
            logger.LogError(ex, "Error calling OpenAI API: {Message}", ex.Message);
            throw;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unexpected error during chat completion: {Message}", ex.Message);
            throw;
        }
    }

    public async IAsyncEnumerable<ChatCompletionChunk> CreateChatCompletionStreaming(
        OpenAiChatCompletionRequest request,
        [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Sending streaming chat completion request to OpenAI API");

        var response = await httpClient.PostAsJsonAsync("v1/chat/completions", request, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            logger.LogError("OpenAI API streaming request failed with status code {StatusCode}: {ErrorContent}",
                response.StatusCode, errorContent);
            throw new HttpRequestException(
                $"OpenAI API request failed with status code {response.StatusCode}: {errorContent}");
        }

        var stream = await response.Content.ReadAsStreamAsync(cancellationToken);
        using var reader = new StreamReader(stream);

        while (!reader.EndOfStream && !cancellationToken.IsCancellationRequested)
        {
            var line = await reader.ReadLineAsync(cancellationToken);

            if (string.IsNullOrEmpty(line))
            {
                continue;
            }

            if (line == "data: [DONE]")
            {
                break;
            }

            if (line.StartsWith("data: "))
            {
                var jsonData = line.Substring("data: ".Length);

                var chunkResponse = JsonSerializer.Deserialize<ChatCompletionChunk>(jsonData);

                if (chunkResponse != null)
                {
                    yield return chunkResponse;
                }
            }
        }
    }
}