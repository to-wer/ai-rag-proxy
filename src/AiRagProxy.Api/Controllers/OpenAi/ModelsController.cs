using AiRagProxy.Api.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[ApiVersion(1)]
public class ModelsController(IChatCompletionProvider chatCompletionProvider) : OpenAiBaseController
{
    [HttpGet]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetModels(CancellationToken cancellationToken = default)
    {
        var models = await chatCompletionProvider.GetModels(cancellationToken);
        return Ok(models);
    }
}