
using AiRagProxy.Api.Data;
using AiRagProxy.Api.Dtos;
using AiRagProxy.Api.Entities;
using AiRagProxy.Api.Services;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
public class ProviderConfigurationController(ApplicationDbContext context, IEncryptionService encryptionService)
    : BaseController
{
    [HttpPost]
    [MapToApiVersion(1)]
    [Authorize]
    public async Task<IActionResult> CreateProviderConfiguration([FromBody] CreateProviderConfigurationDto dto)
    {
        var providerConfig = new ProviderConfiguration
        {
            Name = dto.Name,
            ProviderType = dto.ProviderType,
            AuthenticationType = dto.AuthenticationType,
            BaseUrl = dto.BaseUrl,
            TenantId = dto.TenantId,
            EncryptedSecret = !string.IsNullOrEmpty(dto.ApiKey) 
                ? encryptionService.Encrypt(dto.ApiKey) 
                : null,
            Models = dto.Models.Select(m => new ModelConfiguration
            {
                ModelName = m.ModelName,
                DeploymentName = m.DeploymentName
            }).ToList()
        };

        context.ProviderConfigurations.Add(providerConfig);
        await context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetProviderConfiguration), new { id = providerConfig.Id }, null);
    }

    // Platzhalter für die Get-Methode, die wir als nächstes implementieren
    [HttpGet("{id}")]
    public IActionResult GetProviderConfiguration(int id)
    {
        return Ok(new { Message = "Not implemented yet" });
    }
}
