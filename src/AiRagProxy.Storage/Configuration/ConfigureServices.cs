using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AiRagProxy.Storage.Configuration;

public static class ConfigureServices
{
    public static IServiceCollection AddStorageServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<Context.AiRagProxyContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<Context.IAiRagProxyContext, Context.AiRagProxyContext>();
        services.AddScoped<Services.Interfaces.IAiRagProxyStorageService, Services.AiRagProxyStorageService>();

        return services;
    }
}