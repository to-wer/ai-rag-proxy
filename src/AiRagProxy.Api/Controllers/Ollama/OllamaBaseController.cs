using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.Ollama;

[Route("ollama/[controller]")]
[Authorize]
public abstract class OllamaBaseController : BaseController
{
    
}