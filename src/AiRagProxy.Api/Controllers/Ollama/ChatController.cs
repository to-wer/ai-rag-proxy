using AiRagProxy.Domain.Dtos.Ollama;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.Ollama;

public class ChatController : OllamaBaseController
{
    [HttpPost]
    public IActionResult Post([FromBody] OllamaChatCompletionRequest request)
    {
        throw new NotImplementedException();
    }
}