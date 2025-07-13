
using AiRagProxy.Api.Dtos;
using AiRagProxy.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class ProviderConfigurationController(IProviderConfigurationService providerConfigurationService)
    : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateProviderConfiguration([FromBody] CreateProviderConfigurationDto dto)
    {
        var providerConfig = await providerConfigurationService.CreateProviderConfigurationAsync(dto);
        return CreatedAtAction(nameof(GetProviderConfiguration), new { id = providerConfig.Id }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetProviderConfiguration(int id)
    {
        var config = await providerConfigurationService.GetProviderConfigurationByIdAsync(id);
        if (config == null)
        {
            return NotFound();
        }
        return Ok(config);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProviderConfigurations()
    {
        var configs = await providerConfigurationService.GetAllProviderConfigurationsAsync();
        return Ok(configs);
    }
}
