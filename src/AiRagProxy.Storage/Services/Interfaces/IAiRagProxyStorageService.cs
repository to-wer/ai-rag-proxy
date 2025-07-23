using AiRagProxy.Storage.Entities;

namespace AiRagProxy.Storage.Services.Interfaces;

public interface IAiRagProxyStorageService
{
    Task MigrateDatabase();

    #region User Management
    
    Task<bool> UserExists(string externalId, string issuer);
    Task AddUser(string sub, string issuer, string email, string? name);
    Task UpdateUser(string sub, string issuer, string email, string? name);
    Task<Guid?> GetUserIdBySubject(string sub, string issuer);
    
    #region Personal Access Tokens
    
    Task<PersonalAccessToken> SavePersonalAccessToken(string hash, string name, DateTime? expiresAt, Guid userId);
    Task<List<PersonalAccessToken>> GetPersonalAccessTokens(Guid userId);
    Task<PersonalAccessToken?> GetPersonalAccessToken(string tokenHash);
    
    #endregion Personal Access Tokens
    
    #endregion User Management

    
}