using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[ApiVersion(1)]
[Route("compatibility/openai/v{version:apiVersion}/chat/completions")]
public class ChatCompletionsController : BaseController
{
    [HttpPost]
    [Authorize]
    [MapToApiVersion(1)]
    public IActionResult Post([FromBody] ChatCompletionRequest request)
    {
        // Hier würde die eigentliche Chat-Completion-Logik stehen
        // Beispielantwort
        var response = new ChatCompletionResponse
        {
            Message = $"Echo: {request.Message}"
        };
        return Ok(response);
    }
}

public class ChatCompletionRequest
{
    public string Message { get; set; } = string.Empty;
}

public class ChatCompletionResponse
{
    public string Message { get; set; } = string.Empty;
}

