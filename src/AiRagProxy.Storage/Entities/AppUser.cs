namespace AiRagProxy.Storage.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    public required string ExternalId { get; set; }
    public string Email { get; set; } = string.Empty;
    public string? DisplayName { get; set; }
    public DateTime LastSeen { get; set; }
}