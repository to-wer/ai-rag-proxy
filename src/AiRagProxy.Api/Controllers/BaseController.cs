using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Controllers;

/// <summary>
/// Serves as the base class for all API controllers in the application.
/// Provides common functionality for derived controllers.
/// </summary>
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public abstract class BaseController : ControllerBase
{
    // This class is intentionally left empty as a base for other controllers.
}