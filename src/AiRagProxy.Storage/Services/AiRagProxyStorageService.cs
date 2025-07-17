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

    public async Task<bool> UserExists(string externalId)
    {
        return await context.AppUsers.AnyAsync(u => u.ExternalId == externalId);
    }

    public async Task AddUser(string sub, string email, string? name)
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
            LastSeen = DateTime.UtcNow
        };

        await context.AppUsers.AddAsync(user);
        await context.SaveChangesAsync();
    }

    public async Task UpdateUser(string sub, string email, string? name)
    {
        var user = context.AppUsers.FirstOrDefault(u => u.ExternalId == sub);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with external ID '{sub}' not found.");
        }
        user.Email = email;
        user.DisplayName = name;
        user.LastSeen = DateTime.UtcNow;
        await context.SaveChangesAsync();
    }
}