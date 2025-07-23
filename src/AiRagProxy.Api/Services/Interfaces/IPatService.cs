using AiRagProxy.Domain.Dtos.PersonalAccessToken;

namespace AiRagProxy.Api.Services.Interfaces;

public interface IPatService
{
    Task<CreateTokenResponse> CreateTokenAsync(Guid userId, CreateTokenRequest request);
    Task<List<TokenResponse>> GetTokensAsync(Guid userId);
    Task DeleteTokenAsync(Guid tokenId);
}