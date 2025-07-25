using System.ComponentModel.DataAnnotations;
using AiRagProxy.Domain.Enums;

namespace AiRagProxy.Storage.Entities;

public class ProviderConnection
{
    public Guid Id { get; set; }

    [StringLength(128)] public required string Name { get; set; }
    public ProviderType Type { get; set; }

    [StringLength(256)] public required string ApiUrl { get; set; }
    [StringLength(256)] public string? ApiKeyHash { get; set; }
    public bool Public { get; set; } = false;

    public Guid UserId { get; set; }
    public AppUser? User { get; set; }
}