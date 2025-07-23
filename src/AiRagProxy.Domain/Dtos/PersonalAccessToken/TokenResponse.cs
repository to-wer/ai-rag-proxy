namespace AiRagProxy.Domain.Dtos.PersonalAccessToken;

public class TokenResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
}