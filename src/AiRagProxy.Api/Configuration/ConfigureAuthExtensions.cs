using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace AiRagProxy.Api.Configuration;

/// <summary>
/// Provides extension methods for configuring authentication and authorization in the application.
/// </summary>
public static class ConfigureAuthExtensions
{
    /// <summary>
    /// Configures authentication and authorization for the application.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the authentication and authorization services to.</param>
    /// <param name="configuration">The <see cref="IConfiguration"/> containing the application's configuration settings.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var oidcConfig = configuration.GetSection("Oidc");
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = oidcConfig["Authority"];
                options.Audience = oidcConfig["Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddAuthorization();

        return services;
    }
}