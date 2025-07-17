using AiRagProxy.Api.Services;
using AiRagProxy.Api.Services.Interfaces;

namespace AiRagProxy.Api.Configuration;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}