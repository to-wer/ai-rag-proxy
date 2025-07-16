using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace AiRagProxy.Api.Middlewares;

/// <summary>
/// Middleware for handling exceptions and returning standardized error responses.
/// </summary>
public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ErrorHandlingMiddleware> _logger;

    /// <summary>
    /// Initializes a new instance of the <see cref="ErrorHandlingMiddleware"/> class.
    /// </summary>
    /// <param name="next">The next middleware in the pipeline.</param>
    /// <param name="logger">The logger instance for logging errors.</param>
    public ErrorHandlingMiddleware(RequestDelegate next, ILogger<ErrorHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    /// <summary>
    /// Invokes the middleware to process the HTTP request and handle any exceptions.
    /// </summary>
    /// <param name="context">The <see cref="HttpContext"/> for the current request.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Passes the request to the next middleware in the pipeline.
            await _next(context);
        }
        catch (Exception ex)
        {
            // Logs the exception and prepares a standardized error response.
            _logger.LogError(ex, "An unhandled exception occurred while processing the request.");

            context.Response.ContentType = "application/problem+json";

            var problemDetails = new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc9110#section-15.5.1",
                Title = "An unexpected error occurred.",
                Status = StatusCodes.Status500InternalServerError,
                Detail = ex.Message,
                Instance = context.Request.Path
            };

            context.Response.StatusCode = problemDetails.Status ?? 500;
            await context.Response.WriteAsync(JsonSerializer.Serialize(problemDetails));
        }
    }
}