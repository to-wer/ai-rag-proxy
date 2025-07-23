namespace AiRagProxy.Domain.Dtos.PersonalAccessToken;

public class ValidatedToken
{
    public required Guid TokenId { get; init; }
    public Guid UserId { get; init; }
    public string? TeamId { get; init; }
    public string[] Scopes { get; init; } = [];
    public DateTime? ExpiresAt { get; init; }

    public required string ExternalId { get; set; }
    public required string Email { get; set; }
    public string? DisplayName { get; set; }
}