using System.ComponentModel.DataAnnotations;

namespace AiRagProxy.Domain.Dtos.PersonalAccessToken;

public class CreateTokenRequest
{
    [Required] public required string Name { get; set; }
    public int? ExpireDays { get; set; }
}