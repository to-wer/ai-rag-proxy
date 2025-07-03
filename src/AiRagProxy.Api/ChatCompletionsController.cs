using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api;

[ApiController]
[Route("api/v{version:apiVersion}/chat/completions")]
[ApiVersion("1.0")]
public class ChatCompletionsController : ControllerBase
{
    [HttpPost]
    [Authorize]
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

