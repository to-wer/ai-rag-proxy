using System.Security.Claims;
using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Storage.Services.Interfaces;

namespace AiRagProxy.Api.Services;

public class UserService(IAiRagProxyStorageService storageService) : IUserService
{
    public async Task SyncUser(ClaimsPrincipal principal)
    {
        var sub = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? principal.FindFirst("sub")?.Value;
        var email = principal.FindFirst(ClaimTypes.Email)?.Value ?? "unknown";
        var name = principal.FindFirst(ClaimTypes.Name)?.Value ?? "unknown";

        var issuer = principal.FindFirst("iss")?.Value ?? "unknown";
        
        if (string.IsNullOrEmpty(sub))
        {
            throw new Exception("Mising subject (sub) claim in token.");
        }

        if (!await storageService.UserExists(sub, issuer))
        {
            await storageService.AddUser(sub, issuer, email, name);
        }
        else
        {
            await storageService.UpdateUser(sub, issuer, email, name);
        }
    }

    public async Task<Guid?> GetCurrentUserId(ClaimsPrincipal principal)
    {
        var sub = principal.FindFirstValue(ClaimTypes.NameIdentifier) ??
                  principal.FindFirstValue("sub");
        var issuer = principal.FindFirst("iss")?.Value ?? "unknown";

        if (string.IsNullOrEmpty(sub))
        {
            throw new Exception("Missing subject (sub) claim in token");
        }

        return await storageService.GetUserIdBySubject(sub, issuer);
    }
}