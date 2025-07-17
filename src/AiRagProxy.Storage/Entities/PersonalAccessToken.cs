namespace AiRagProxy.Storage.Entities;

public class PersonalAccessToken
{
    public Guid Id { get; set; }
    public required string TokenHash { get; set; }
    public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Guid AppUserId { get; set; }
}