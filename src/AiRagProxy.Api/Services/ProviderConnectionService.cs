using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.ProviderConnection;
using AiRagProxy.Storage.Entities;
using AiRagProxy.Storage.Services.Interfaces;

namespace AiRagProxy.Api.Services;

public class ProviderConnectionService(IAiRagProxyStorageService storageService) : IProviderConnectionService
{
    public async Task<IEnumerable<ProviderConnection>> GetProviderConnectionsAsync(Guid userId, bool isAdmin = false)
    {
        return await storageService.GetProviderConnections(userId, isAdmin);
    }

    public async Task<ProviderConnection?> GetProviderConnectionAsync(Guid userId, string providerName)
    {
        var providerConnection = await storageService.GetProviderConnection(userId, providerName);
        // TODO: decrypt api key
        return providerConnection;
    }

    public async Task CreateProviderConnectionAsync(Guid userId, CreateProviderConnectionRequest request)
    {
        // TODO: encrypt API key
        var encryptedApiKey = request.ApiKey; // Placeholder for actual encryption logic
        
        await storageService.SaveProviderConnection(userId, request.Name, request.ProviderType, request.ApiUrl,
            encryptedApiKey, request.IsPublic);
    }
}