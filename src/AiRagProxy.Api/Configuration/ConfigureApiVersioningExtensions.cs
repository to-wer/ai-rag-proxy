using Asp.Versioning;

namespace AiRagProxy.Api.Configuration;

/// <summary>
/// Provides extension methods for configuring API versioning in the application.
/// </summary>
public static class ConfigureApiVersioningExtensions
{
    /// <summary>
    /// Configures API versioning for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the API versioning services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ConfigureApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1);
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddMvc()
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'V";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }
}