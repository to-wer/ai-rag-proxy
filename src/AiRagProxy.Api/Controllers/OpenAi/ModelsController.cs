using AiRagProxy.Api.Services.Interfaces;
using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.OpenAi;

[ApiVersion(1)]
public class ModelsController(IOpenAiCommunicationService openAiCommunicationService) : OpenAiBaseController
{
    [HttpGet]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetModels(CancellationToken cancellationToken = default)
    {
        var models = await openAiCommunicationService.GetModels(cancellationToken);
        return Ok(models);
    }
}