using System.ComponentModel.DataAnnotations;

namespace AiRagProxy.Storage.Entities;

public class AppUser
{
    public Guid Id { get; set; }
    [StringLength(128)] public string ExternalId { get; set; } = string.Empty;
    [StringLength(128)] public string Provider { get; set; } = string.Empty;
    [StringLength(256)] public string Email { get; set; } = string.Empty;
    [StringLength(128)] public string? DisplayName { get; set; }
    public DateTime? LastSeen { get; set; }

    public List<PersonalAccessToken> Tokens { get; set; } = [];
}