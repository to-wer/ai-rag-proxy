using System.Text.Json;
using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.OpenAi;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[ApiVersion(1)]
public class ChatController(IOpenAiCommunicationService communicationService) : OpenAiBaseController
{
    [HttpPost("completions")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> Completions([FromBody] OpenAiChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        if (request.Stream)
        {
            Response.ContentType = "text/event-stream";
            
            await foreach (var chunk in communicationService.CreateChatCompletionStreaming(request, cancellationToken))
            {
                var json = JsonSerializer.Serialize(chunk);
                await Response.WriteAsync($"data: {json}\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
            return new EmptyResult();
        }
        var response = await communicationService.CreateChatCompletion(request);
        return Ok(response);
    }
}