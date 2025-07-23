using AiRagProxy.Domain.Dtos.PersonalAccessToken;

namespace AiRagProxy.Api.Services.Interfaces;

public interface ITokenValidationService
{
    ValidatedToken? ValidateToken(string token);
}