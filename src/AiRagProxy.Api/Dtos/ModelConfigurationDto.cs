
namespace AiRagProxy.Api.Dtos;

public class ModelConfigurationDto
{
    public int Id { get; set; }
    public string ModelName { get; set; } = string.Empty;
    public string DeploymentName { get; set; } = string.Empty;
}
