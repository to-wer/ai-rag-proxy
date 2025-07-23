using AiRagProxy.Storage.Context;
using AiRagProxy.Storage.Entities;
using AiRagProxy.Storage.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace AiRagProxy.Storage.Services;

public class AiRagProxyStorageService(IAiRagProxyContext context) : IAiRagProxyStorageService
{
    public async Task MigrateDatabase()
    {
        try
        {
            await ((DbContext)context).Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            // Log the exception (logging not implemented in this example)
            Console.WriteLine($"Database migration failed: {ex.Message}");
            throw;
        }
    }

    #region User Management

    public async Task<bool> UserExists(string externalId, string issuer)
    {
        return await context.AppUsers.AnyAsync(u => u.ExternalId == externalId && u.Provider == issuer);
    }

    public async Task AddUser(string sub, string issuer, string email, string? name)
    {
        if (string.IsNullOrEmpty(sub))
        {
            throw new ArgumentException("Subject (sub) cannot be null or empty.", nameof(sub));
        }

        var user = new AppUser
        {
            ExternalId = sub,
            Email = email,
            DisplayName = name,
            LastSeen = DateTime.UtcNow,
            Provider = issuer
        };

        await context.AppUsers.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUser(string sub, string issuer, string email, string? name)
    {
        var user = context.AppUsers.FirstOrDefault(u => u.ExternalId == sub && u.Provider == issuer);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with external ID '{sub}' not found.");
        }

        user.Email = email;
        user.DisplayName = name;
        user.LastSeen = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }

    public async Task<Guid?> GetUserIdBySubject(string sub, string issuer)
    {
        var user = await context.AppUsers
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.ExternalId == sub && u.Provider == issuer);

        return user?.Id;
    }

    #region Personal Access Tokens

    public async Task<PersonalAccessToken> SavePersonalAccessToken(string hash, string name, DateTime? expiresAt,
        Guid userId)
    {
        var token = new PersonalAccessToken
        {
            Id = Guid.NewGuid(),
            TokenHash = hash,
            Name = name,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = expiresAt,
            UserId = userId
        };

        await context.PersonalAccessTokens.AddAsync(token);
        await context.SaveChangesAsync();

        return token;
    }

    public async Task<List<PersonalAccessToken>> GetPersonalAccessTokens(Guid userId)
    {
        return await context.PersonalAccessTokens
            .Where(x => x.UserId == userId)
            .ToListAsync();
    }

    public async Task<PersonalAccessToken?> GetPersonalAccessToken(string tokenHash)
    {
        return await context.PersonalAccessTokens
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash
                                      && (x.ExpiresAt == null || x.ExpiresAt > DateTime.UtcNow));
    }

    #endregion Personal Access Tokens

    #endregion User Management
}