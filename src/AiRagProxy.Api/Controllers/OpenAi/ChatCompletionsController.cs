using AiRagProxy.Api.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[ApiVersion(1)]
[Route("compatibility/openai/v{version:apiVersion}/chat/completions")]
public class ChatCompletionsController : BaseController
{
    private readonly IProviderConfigurationService _providerConfigurationService;

    public ChatCompletionsController(IProviderConfigurationService providerConfigurationService)
    {
        _providerConfigurationService = providerConfigurationService;
    }

    [HttpPost]
    [Authorize]
    [MapToApiVersion(1)]
    public async Task<IActionResult> Post([FromBody] ChatCompletionRequest request)
    {
        var providerConfig = await _providerConfigurationService.GetProviderConfigurationByModelAsync(request.Model);

        if (providerConfig == null)
        {
            return NotFound($"No provider configuration found for model {request.Model}");
        }

        // Hier würde die eigentliche Chat-Completion-Logik stehen
        // Beispielantwort
        var response = new ChatCompletionResponse
        {
            Message = $"Provider for model {request.Model} is {providerConfig.Name}"
        };
        return Ok(response);
    }
}

public class ChatCompletionRequest
{
    public string Model { get; set; } = string.Empty;
}

public class ChatCompletionResponse
{
    public string Message { get; set; } = string.Empty;
}

