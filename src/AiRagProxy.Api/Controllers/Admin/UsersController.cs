using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers.Admin;

[Authorize(Roles = "Admin")]
[ApiVersion(1)]
public class UsersController : BaseController
{
    [HttpGet]
    [MapToApiVersion(1)]
    public IActionResult Get()
    {
        throw new NotImplementedException("This endpoint is not implemented yet.");
    }
}