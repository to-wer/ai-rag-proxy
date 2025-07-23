namespace AiRagProxy.Api.Services.Interfaces;

public interface ITokenGeneratorService
{
    string GenerateToken();
    string HashToken(string token);
}