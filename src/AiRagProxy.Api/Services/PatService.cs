using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.PersonalAccessToken;
using AiRagProxy.Storage.Entities;
using AiRagProxy.Storage.Services.Interfaces;

namespace AiRagProxy.Api.Services;

public class PatService(IAiRagProxyStorageService storageService,
    ITokenGeneratorService tokenGenerator) : IPatService
{
    public async Task<CreateTokenResponse> CreateTokenAsync(Guid userId, CreateTokenRequest request)
    {
        var plainToken = tokenGenerator.GenerateToken();
        var hash = tokenGenerator.HashToken(plainToken);

        DateTime? expiresAt = request.ExpireDays.HasValue
            ? DateTime.UtcNow.AddDays(request.ExpireDays.Value)
            : null;

        PersonalAccessToken token = await storageService.SavePersonalAccessToken(hash, request.Name, expiresAt, userId);

        return new CreateTokenResponse()
        {
            Id = token.Id,
            Name = token.Name,
            PlaintextToken = plainToken,
            CreatedAt = token.CreatedAt,
            ExpiresAt = token.ExpiresAt
        };
    }

    public async Task<List<TokenResponse>> GetTokensAsync(Guid userId)
    {
        var personalAccessTokens = await storageService.GetPersonalAccessTokens(userId);
        return personalAccessTokens.Select(x =>
            new TokenResponse
            {
                Id = x.Id, Name = x.Name, CreatedAt = x.CreatedAt, ExpiresAt = x.ExpiresAt,
            }
        ).ToList();
    }

    public async Task DeleteTokenAsync(Guid tokenId)
    {
        throw new NotImplementedException();
    }
}