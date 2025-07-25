using AiRagProxy.Domain.Enums;

namespace AiRagProxy.Domain.Dtos.ProviderConnection;

public class CreateProviderConnectionRequest
{
    public required string Name { get; set; }
    public required string ApiUrl { get; set; }
    public ProviderType ProviderType { get; set; }
    public string? ApiKey { get; set; }
    public bool IsPublic { get; set; }
}