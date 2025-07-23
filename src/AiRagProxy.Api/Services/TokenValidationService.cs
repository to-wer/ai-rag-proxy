using System.Security.Cryptography;
using System.Text;
using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.PersonalAccessToken;
using AiRagProxy.Storage.Services.Interfaces;

namespace AiRagProxy.Api.Services;

public class TokenValidationService(IAiRagProxyStorageService storageService) : ITokenValidationService
{
    public async Task<ValidatedToken?> ValidateToken(string token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return null;
        }

        var tokenHash = ComputeSha256Hash(token);

        var pat = await storageService.GetPersonalAccessToken(tokenHash);

        if (pat?.User == null)
        {
            return null;
        }
		
        var validatedToken = new ValidatedToken
        {
            TokenId = pat.Id,
            UserId = pat.UserId,
            TeamId = null,
            ExpiresAt = pat.ExpiresAt,
            ExternalId = pat.User.ExternalId,
            Email = pat.User.Email,
            DisplayName = pat.User.DisplayName
        };
		
        return validatedToken;
    }
	
    private static string ComputeSha256Hash(string rawData)
    {
        using var sha256 = SHA256.Create();
        var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(rawData));
        return Convert.ToHexString(bytes);
    }
}