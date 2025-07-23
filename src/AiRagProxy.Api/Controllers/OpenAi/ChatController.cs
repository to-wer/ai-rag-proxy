using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.OpenAi;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[ApiVersion(1)]
public class ChatController(IOpenAiChatCompletionService chatCompletionService) : OpenAiBaseController
{
    [HttpPost("completions")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> Completions([FromBody] OpenAiChatCompletionRequest request)
    {
        if (request.Stream)
        {
            Response.ContentType = "text/event-stream";
            
            await foreach (var chunk in chatCompletionService.CreateChatCompletionStreaming(request))
            {
                if (Response.HttpContext.RequestAborted.IsCancellationRequested)
                {
                    break;
                }

                await Response.WriteAsJsonAsync(chunk);
                await Response.Body.FlushAsync();
            }
        }
        var response = await chatCompletionService.CreateChatCompletion(request);
        return Ok(response);
    }
}