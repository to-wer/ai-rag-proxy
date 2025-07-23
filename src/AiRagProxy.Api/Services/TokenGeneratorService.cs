using System.Security.Cryptography;
using System.Text;
using AiRagProxy.Api.Services.Interfaces;

namespace AiRagProxy.Api.Services;

public class TokenGeneratorService : ITokenGeneratorService
{
    public string GenerateToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32);
        return Convert.ToBase64String(bytes);
    }

    public string HashToken(string token)
    {
        using var sha = SHA256.Create();
        var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(token));
        return Convert.ToHexString(bytes);
    }
}