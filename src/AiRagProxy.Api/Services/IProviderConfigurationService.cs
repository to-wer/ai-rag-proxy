
using AiRagProxy.Api.Dtos;
using AiRagProxy.Api.Entities;

namespace AiRagProxy.Api.Services;

public interface IProviderConfigurationService
{
    Task<ProviderConfiguration> CreateProviderConfigurationAsync(CreateProviderConfigurationDto dto);
    Task<ProviderConfigurationDto?> GetProviderConfigurationByIdAsync(int id);
    Task<IEnumerable<ProviderConfigurationDto>> GetAllProviderConfigurationsAsync();
    Task<ProviderConfiguration?> GetProviderConfigurationByModelAsync(string modelName);
}
