using AiRagProxy.Api.Services.Interfaces;
using AiRagProxy.Domain.Dtos.PersonalAccessToken;
using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers;

[Authorize]
[ApiVersion(1)]
public class PersonalAccessTokensController(IPatService patService,
    IUserService userService) : BaseController
{
    [HttpPost]
    [MapToApiVersion(1)]
    public async Task<ActionResult<CreateTokenResponse>> Create([FromBody] CreateTokenRequest request)
    {
        var userId = await userService.GetCurrentUserId(User);
        if (userId == null)
        {
            return Unauthorized();
        }

        var token = await patService.CreateTokenAsync(userId.Value, request);
        return Ok(token);
    }

}