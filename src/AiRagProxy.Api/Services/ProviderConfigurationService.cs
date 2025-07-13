
using AiRagProxy.Api.Data;
using AiRagProxy.Api.Dtos;
using AiRagProxy.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiRagProxy.Api.Services;

public class ProviderConfigurationService : IProviderConfigurationService
{
    private readonly ApplicationDbContext _context;
    private readonly IEncryptionService _encryptionService;

    public ProviderConfigurationService(ApplicationDbContext context, IEncryptionService encryptionService)
    {
        _context = context;
        _encryptionService = encryptionService;
    }

    public async Task<ProviderConfiguration> CreateProviderConfigurationAsync(CreateProviderConfigurationDto dto)
    {
        var providerConfig = new ProviderConfiguration
        {
            Name = dto.Name,
            ProviderType = dto.ProviderType,
            AuthenticationType = dto.AuthenticationType,
            BaseUrl = dto.BaseUrl,
            TenantId = dto.TenantId,
            EncryptedSecret = !string.IsNullOrEmpty(dto.ApiKey) 
                ? _encryptionService.Encrypt(dto.ApiKey) 
                : null,
            Models = dto.Models.Select(m => new ModelConfiguration
            {
                ModelName = m.ModelName,
                DeploymentName = m.DeploymentName
            }).ToList()
        };

        _context.ProviderConfigurations.Add(providerConfig);
        await _context.SaveChangesAsync();

        return providerConfig;
    }

    public async Task<ProviderConfigurationDto?> GetProviderConfigurationByIdAsync(int id)
    {
        var config = await _context.ProviderConfigurations
            .Include(pc => pc.Models)
            .FirstOrDefaultAsync(pc => pc.Id == id);

        if (config == null)
        {
            return null;
        }

        return new ProviderConfigurationDto
        {
            Id = config.Id,
            Name = config.Name,
            ProviderType = config.ProviderType,
            AuthenticationType = config.AuthenticationType,
            BaseUrl = config.BaseUrl,
            TenantId = config.TenantId,
            Models = config.Models.Select(m => new ModelConfigurationDto
            {
                Id = m.Id,
                ModelName = m.ModelName,
                DeploymentName = m.DeploymentName
            }).ToList()
        };
    }

    public async Task<IEnumerable<ProviderConfigurationDto>> GetAllProviderConfigurationsAsync()
    {
        var configs = await _context.ProviderConfigurations
            .Include(pc => pc.Models)
            .ToListAsync();

        return configs.Select(config => new ProviderConfigurationDto
        {
            Id = config.Id,
            Name = config.Name,
            ProviderType = config.ProviderType,
            AuthenticationType = config.AuthenticationType,
            BaseUrl = config.BaseUrl,
            TenantId = config.TenantId,
            Models = config.Models.Select(m => new ModelConfigurationDto
            {
                Id = m.Id,
                ModelName = m.ModelName,
                DeploymentName = m.DeploymentName
            }).ToList()
        });
    }
}
