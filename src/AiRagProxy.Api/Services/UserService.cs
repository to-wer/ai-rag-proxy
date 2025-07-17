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

        if (string.IsNullOrEmpty(sub))
        {
            throw new Exception("Mising subject (sub) claim in token.");
        }

        if (!await storageService.UserExists(sub))
        {
            await storageService.AddUser(sub, email, name);
        }
        else
        {
            await storageService.UpdateUser(sub, email, name);
        }
    }
}