using Microsoft.AspNetCore.RateLimiting;

namespace AiRagProxy.Api.Configuration;

/// <summary>
/// Provides extension methods for configuring rate limiting in the application.
/// </summary>
public static class ConfigureRateLimitingExtensions
{
    /// <summary>
    /// Configures rate limiting for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the rate limiting services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing the application's configuration settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    /// <exception cref="ArgumentNullException">Thrown when rate limiting options are not configured.</exception>
    public static IServiceCollection ConfigureRateLimiting(this IServiceCollection services,
        IConfiguration configuration)
    {
        var rateLimitingOptions = configuration.GetSection("RateLimiting").Get<RateLimitingOptions>();
        if (rateLimitingOptions == null)
        {
            throw new ArgumentNullException(nameof(rateLimitingOptions), "Rate limiting options are not configured.");
        }

        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("default", builder =>
            {
                builder.PermitLimit = rateLimitingOptions.PermitLimit;
                builder.Window = TimeSpan.FromMinutes(rateLimitingOptions.WindowMinutes);
                builder.QueueLimit = rateLimitingOptions.QueueLimit;
            });
        });

        return services;
    }
}