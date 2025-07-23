using System.ComponentModel.DataAnnotations;

namespace AiRagProxy.Storage.Entities;

public class PersonalAccessToken
{
    public Guid Id { get; set; }
    [StringLength(128)] public required string TokenHash { get; set; }
    [StringLength(128)] public required string Name { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? ExpiresAt { get; set; }
    public Guid AppUserId { get; set; }
}