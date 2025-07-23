using AiRagProxy.Domain.Dtos.PersonalAccessToken;

namespace AiRagProxy.Api.Services.Interfaces;

public interface ITokenValidationService
{
    Task<ValidatedToken?> ValidateToken(string token);
}