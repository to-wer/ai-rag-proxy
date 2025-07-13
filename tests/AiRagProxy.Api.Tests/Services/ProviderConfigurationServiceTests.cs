
using AiRagProxy.Api.Data;
using AiRagProxy.Api.Dtos;
using AiRagProxy.Api.Entities;
using AiRagProxy.Api.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace AiRagProxy.Api.Tests.Services;

public class ProviderConfigurationServiceTests
{
    private ApplicationDbContext GetInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlite("DataSource=:memory:")
            .Options;
        var context = new ApplicationDbContext(options);
        context.Database.OpenConnection();
        context.Database.EnsureCreated();
        return context;
    }

    [Fact]
    public async Task CreateProviderConfigurationAsync_ShouldEncryptApiKeyAndSaveToDb()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockEncryptionService = new Mock<IEncryptionService>();
        mockEncryptionService.Setup(s => s.Encrypt(It.IsAny<string>())).Returns((string plainText) => "Encrypted_" + plainText);

        var service = new ProviderConfigurationService(context, mockEncryptionService.Object);

        var createDto = new CreateProviderConfigurationDto
        {
            Name = "Test OpenAI",
            ProviderType = ProviderType.OpenAI,
            AuthenticationType = AuthenticationType.ApiKey,
            BaseUrl = "https://api.openai.com",
            ApiKey = "sk-testkey",
            TenantId = "tenant1",
            Models = new List<CreateModelConfigurationDto>
            {
                new() { ModelName = "gpt-3.5-turbo", DeploymentName = "gpt-35-turbo" }
            }
        };

        // Act
        var result = await service.CreateProviderConfigurationAsync(createDto);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(createDto.Name, result.Name);
        Assert.Equal("Encrypted_sk-testkey", result.EncryptedSecret);
        Assert.Single(result.Models);
        Assert.Equal(createDto.Models[0].ModelName, result.Models[0].ModelName);

        var savedConfig = await context.ProviderConfigurations.Include(pc => pc.Models).FirstOrDefaultAsync();
        Assert.NotNull(savedConfig);
        Assert.Equal(createDto.Name, savedConfig.Name);
        Assert.Equal("Encrypted_sk-testkey", savedConfig.EncryptedSecret);
        Assert.Single(savedConfig.Models);
    }

    [Fact]
    public async Task GetProviderConfigurationByIdAsync_ShouldReturnCorrectDto()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockEncryptionService = new Mock<IEncryptionService>();
        var service = new ProviderConfigurationService(context, mockEncryptionService.Object);

        var providerConfig = new ProviderConfiguration
        {
            Name = "Existing Provider",
            ProviderType = ProviderType.AzureOpenAI,
            AuthenticationType = AuthenticationType.BearerToken,
            BaseUrl = "https://azure.openai.com",
            EncryptedSecret = "Encrypted_AzureKey",
            TenantId = "tenant2",
            Models = new List<ModelConfiguration>
            {
                new() { ModelName = "gpt-4", DeploymentName = "gpt4-deployment" }
            }
        };
        context.ProviderConfigurations.Add(providerConfig);
        await context.SaveChangesAsync();

        // Act
        var result = await service.GetProviderConfigurationByIdAsync(providerConfig.Id);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(providerConfig.Name, result.Name);
        Assert.Equal(providerConfig.ProviderType, result.ProviderType);
        Assert.Equal(providerConfig.AuthenticationType, result.AuthenticationType);
        Assert.Equal(providerConfig.BaseUrl, result.BaseUrl);
        Assert.Equal(providerConfig.TenantId, result.TenantId);
        Assert.Single(result.Models);
        Assert.Equal(providerConfig.Models[0].ModelName, result.Models[0].ModelName);
        Assert.Equal(providerConfig.Models[0].DeploymentName, result.Models[0].DeploymentName);
    }

    [Fact]
    public async Task GetAllProviderConfigurationsAsync_ShouldReturnAllDtos()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockEncryptionService = new Mock<IEncryptionService>();
        var service = new ProviderConfigurationService(context, mockEncryptionService.Object);

        context.ProviderConfigurations.AddRange(
            new ProviderConfiguration { Name = "P1", ProviderType = ProviderType.OpenAI, AuthenticationType = AuthenticationType.ApiKey, BaseUrl = "url1", EncryptedSecret = "s1" },
            new ProviderConfiguration { Name = "P2", ProviderType = ProviderType.AzureOpenAI, AuthenticationType = AuthenticationType.BearerToken, BaseUrl = "url2", EncryptedSecret = "s2" }
        );
        await context.SaveChangesAsync();

        // Act
        var results = await service.GetAllProviderConfigurationsAsync();

        // Assert
        Assert.NotNull(results);
        var resultsList = results.ToList();
        Assert.Equal(2, resultsList.Count);
        Assert.Contains(resultsList, r => r.Name == "P1");
        Assert.Contains(resultsList, r => r.Name == "P2");
    }

    [Fact]
    public async Task GetProviderConfigurationByIdAsync_ShouldReturnNullForNotFound()
    {
        // Arrange
        using var context = GetInMemoryDbContext();
        var mockEncryptionService = new Mock<IEncryptionService>();
        var service = new ProviderConfigurationService(context, mockEncryptionService.Object);

        // Act
        var result = await service.GetProviderConfigurationByIdAsync(999);

        // Assert
        Assert.Null(result);
    }
}
