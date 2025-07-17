namespace AiRagProxy.Storage.Services.Interfaces;

public interface IAiRagProxyStorageService
{
    Task MigrateDatabase();

    Task<bool> UserExists(string externalId);
    Task AddUser(string sub, string email, string? name);
    Task UpdateUser(string sub, string email, string? name);
}