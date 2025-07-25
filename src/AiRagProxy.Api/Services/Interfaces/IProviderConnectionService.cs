using AiRagProxy.Domain.Dtos.ProviderConnection;
using AiRagProxy.Storage.Entities;

namespace AiRagProxy.Api.Services.Interfaces;

public interface IProviderConnectionService
{
    Task<IEnumerable<ProviderConnection>> GetProviderConnectionsAsync(Guid userId, bool isAdmin = false);
    Task<ProviderConnection?> GetProviderConnectionAsync(Guid userId, string providerName);
    Task CreateProviderConnectionAsync(Guid userId, CreateProviderConnectionRequest request);
}