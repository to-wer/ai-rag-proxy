using System.Security.Claims;

namespace AiRagProxy.Api.Services.Interfaces;

public interface IUserService
{
    Task SyncUser(ClaimsPrincipal principal);
}