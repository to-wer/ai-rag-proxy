using AiRagProxy.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiRagProxy.Storage.Context;

public interface IAiRagProxyContext
{
    DbSet<AppUser> AppUsers { get; set; }
    DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }
    DbSet<ProviderConnection> ProviderConnections { get; set; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}