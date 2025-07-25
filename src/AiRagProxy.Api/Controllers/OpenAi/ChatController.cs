using System.Text.Json;
using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.OpenAi;
using AiRagProxy.Domain.Utils;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[ApiVersion(1)]
public class ChatController(IChatCompletionProviderFactory chatCompletionProviderFactory,
    IProviderConnectionService providerConnectionService,
    IUserService userService) : OpenAiBaseController
{
    [HttpPost("completions")]
    [MapToApiVersion(1)]
    public async Task<IActionResult> Completions([FromBody] OpenAiChatCompletionRequest request, CancellationToken cancellationToken = default)
    {
        var (providerName, modelName) = ProviderModelParser.ParseProviderAndModel(request.Model);
        var userId = await userService.GetCurrentUserId(User);
        var providerConnection = await providerConnectionService.GetProviderConnectionAsync(userId ?? Guid.Empty, providerName);
        if (providerConnection == null)
        {
            return NotFound($"Provider connection for '{providerName}' not found.");
        }
        var provider = chatCompletionProviderFactory.GetProvider(providerConnection.Type);
        // TODO: set url and api key
        request.Model = modelName;

        if (request.Stream)
        {
            Response.ContentType = "text/event-stream";
            
            await foreach (var chunk in provider.CreateChatCompletionStreaming(request, cancellationToken))
            {
                var json = JsonSerializer.Serialize(chunk);
                await Response.WriteAsync($"data: {json}\n\n", cancellationToken);
                await Response.Body.FlushAsync(cancellationToken);
            }
            return new EmptyResult();
        }

        var response = await provider.CreateChatCompletion(request);
        return Ok(response);
    }
}