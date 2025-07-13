
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
        var providerConfigDto = new ProviderConfigurationDto
        {
            Id = providerConfig.Id,
            Name = providerConfig.Name,
            ProviderType = providerConfig.ProviderType,
            AuthenticationType = providerConfig.AuthenticationType,
            BaseUrl = providerConfig.BaseUrl,
            TenantId = providerConfig.TenantId,
            Models = providerConfig.Models.Select(m => new ModelConfigurationDto
            {
                Id = m.Id,
                ModelName = m.ModelName,
                DeploymentName = m.DeploymentName
            }).ToList()
        };
        return CreatedAtAction(nameof(GetProviderConfiguration), new { id = providerConfigDto.Id }, providerConfigDto);
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
