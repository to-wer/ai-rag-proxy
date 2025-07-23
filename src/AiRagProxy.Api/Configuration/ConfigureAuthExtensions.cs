using AiRagProxy.Api.Middlewares;
using AiRagProxy.Api.Services.Interfaces;
using Microsoft.AspNetCore.Authentication;
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
    /// <param name="hostEnvironment"></param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services,
        IConfiguration configuration,
        IHostEnvironment hostEnvironment)
    {
        var oidcConfig = configuration.GetSection("Oidc");
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                options.Authority = oidcConfig["Authority"];
                options.Audience = oidcConfig["Audience"];
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = oidcConfig["Authority"],
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
                if (!hostEnvironment.IsDevelopment())
                {
                    options.RequireHttpsMetadata = false;
                }

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = async context =>
                    {
                        var claims = context.Principal;
                        var userService = context.HttpContext.RequestServices.GetRequiredService<IUserService>();
                        if (claims != null) await userService.SyncUser(claims);
                    }
                };
            })
            .AddScheme<AuthenticationSchemeOptions, PatAuthenticationHandler>("PAT", options => { });

        services.AddAuthorization();

        return services;
    }
}