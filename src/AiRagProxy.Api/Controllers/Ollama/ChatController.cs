using AiRagProxy.Domain.Dtos.Ollama;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.Ollama;

public class ChatController : OllamaBaseController
{
    [HttpPost]
    public IActionResult Post([FromBody] OllamaChatCompletionRequest request)
    {
        // This is a placeholder for the actual implementation.
        // You would typically call a service to handle the chat request.
        return Ok(new { message = "Chat request received", data = request });
    }
}