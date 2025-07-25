using AiRagProxy.Domain.Enums;
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

    #region Provider Connections

    Task<List<ProviderConnection>> GetProviderConnections(Guid userId, bool isAdmin = false);
    Task<ProviderConnection?> GetProviderConnection(Guid userId, string name);

    Task SaveProviderConnection(Guid userId, string name, ProviderType providerType, string apiUrl,
        string? encryptedApiKey, bool isPublic);

    #endregion Provider Connections
}