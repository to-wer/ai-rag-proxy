using AiRagProxy.Api.Services;
using AiRagProxy.Api.Services.Interfaces;

namespace AiRagProxy.Api.Configuration;

public static class ConfigureServicesExtensions
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddHttpClient<IOpenAiCommunicationService, OpenAiCommunicationService>(client =>
        {
            client.BaseAddress = new Uri(configuration["OpenAi:BaseUrl"] ?? "https://api.openai.com/v1/");
            
            var apiKey = configuration["OpenAi:ApiKey"];
            if (!string.IsNullOrEmpty(apiKey))
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {apiKey}");
            }
        });
        services.AddScoped<ITokenValidationService, TokenValidationService>();
        services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        services.AddScoped<IPatService, PatService>();

        return services;
    }
}