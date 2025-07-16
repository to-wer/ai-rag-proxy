namespace AiRagProxy.Api.Configuration;

/// <summary>
/// Provides extension methods for configuring CORS in the application.
/// </summary>
public static class ConfigureCorsExtensions
{
    /// <summary>
    /// Configures CORS for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the CORS services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ConfigureCors(this IServiceCollection services)
    {
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAllOrigins",
                builder =>
                {
                    builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
        });

        return services;
    }
}