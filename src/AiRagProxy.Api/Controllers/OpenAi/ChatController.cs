using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[ApiVersion(1)]
public class ChatController : OpenAiBaseController
{
    [HttpPost("completions")]
    [MapToApiVersion(1)]
    public IActionResult Completions([FromBody] ChatCompletionRequest request)
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

