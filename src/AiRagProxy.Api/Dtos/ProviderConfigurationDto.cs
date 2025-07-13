
using System.Collections.Generic;
using AiRagProxy.Api.Entities;

namespace AiRagProxy.Api.Dtos;

public class ProviderConfigurationDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public ProviderType ProviderType { get; set; }
    public AuthenticationType AuthenticationType { get; set; }
    public string BaseUrl { get; set; } = string.Empty;
    public string? TenantId { get; set; }
    public List<ModelConfigurationDto> Models { get; set; } = new();
}
