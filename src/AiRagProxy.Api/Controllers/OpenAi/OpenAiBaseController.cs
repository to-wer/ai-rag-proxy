using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[Route("openai/v{version:apiVersion}/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer,PAT")]
public abstract class OpenAiBaseController : BaseController
{
}