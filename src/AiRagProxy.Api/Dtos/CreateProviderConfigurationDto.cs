
using System.Collections.Generic;
using AiRagProxy.Api.Entities;

namespace AiRagProxy.Api.Dtos;

public class CreateProviderConfigurationDto
{
    public string Name { get; set; } = string.Empty;
    public ProviderType ProviderType { get; set; }
    public AuthenticationType AuthenticationType { get; set; }
    public string BaseUrl { get; set; } = string.Empty;
    public string? ApiKey { get; set; } // Klartext-Key, nur für die Erstellung
    public string? TenantId { get; set; }
    public List<CreateModelConfigurationDto> Models { get; set; } = new();
}

public class CreateModelConfigurationDto
{
    public string ModelName { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
}
