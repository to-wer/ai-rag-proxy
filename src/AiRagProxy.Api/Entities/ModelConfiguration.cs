
namespace AiRagProxy.Api.Entities;

public class ModelConfiguration
{
    public int Id { get; set; }

    public string ModelName { get; set; } = string.Empty;

    public string DeploymentName { get; set; } = string.Empty;

    public int ProviderConfigurationId { get; set; }
    public ProviderConfiguration ProviderConfiguration { get; set; } = null!;
}
