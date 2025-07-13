
using System.Collections.Generic;

namespace AiRagProxy.Api.Entities;

public class ProviderConfiguration
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public ProviderType ProviderType { get; set; }
    public AuthenticationType AuthenticationType { get; set; }

    public string BaseUrl { get; set; } = string.Empty;

    public string? EncryptedSecret { get; set; }

    public string? TenantId { get; set; }

    public List<ModelConfiguration> Models { get; set; } = new();
}
