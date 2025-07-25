using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.ProviderConnection;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.Admin;

//[Authorize(Roles = "Admin")]
[Authorize]
[ApiVersion(1)]
public class ProviderConnectionsController(
    IProviderConnectionService providerConnectionService,
    IUserService userService) : BaseController
{
    [HttpGet]
    [MapToApiVersion(1)]
    public async Task<IActionResult> GetProviderConnections()
    {
        var userId = await userService.GetCurrentUserId(User);
        var providerConnections = await providerConnectionService.GetProviderConnectionsAsync(userId ?? Guid.Empty,
            isAdmin: true);
        return Ok(providerConnections);
    }

    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<IActionResult> CreateProviderConnection([FromBody] CreateProviderConnectionRequest request)
    {
        var userId = await userService.GetCurrentUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }

        await providerConnectionService.CreateProviderConnectionAsync(userId.Value, request);

        return Created();
    }
}