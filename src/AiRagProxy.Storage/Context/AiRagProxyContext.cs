using AiRagProxy.Storage.Entities;
using Microsoft.EntityFrameworkCore;

namespace AiRagProxy.Storage.Context;

public class AiRagProxyContext(DbContextOptions<AiRagProxyContext> options) : DbContext(options), IAiRagProxyContext
{
    public DbSet<AppUser> AppUsers { get; set; }
    public DbSet<PersonalAccessToken> PersonalAccessTokens { get; set; }
    public DbSet<ProviderConnection> ProviderConnections { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}