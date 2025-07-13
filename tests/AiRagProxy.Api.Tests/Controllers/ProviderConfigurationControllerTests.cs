using AiRagProxy.Api.Controllers;
using AiRagProxy.Api.Dtos;
using AiRagProxy.Api.Entities;
using AiRagProxy.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AiRagProxy.Api.Tests.Controllers;

public class ProviderConfigurationControllerTests
{
    private readonly Mock<IProviderConfigurationService> _providerConfigurationServiceMock;
    private readonly ProviderConfigurationController _controller;
    
    public ProviderConfigurationControllerTests()
    {
        _providerConfigurationServiceMock = new Mock<IProviderConfigurationService>();
        _controller = new ProviderConfigurationController(_providerConfigurationServiceMock.Object);

    }
    
    [Fact]
public async Task CreateProviderConfiguration_ReturnsCreatedAtAction_WhenDtoIsValid()
{
    // Arrange
    var dto = new CreateProviderConfigurationDto { Name = "TestProvider", BaseUrl = "http://test.com", ProviderType = ProviderType.OpenAI, AuthenticationType = AuthenticationType.ApiKey };
    var createdConfig = new ProviderConfiguration { Id = 1, Name = "TestProvider", BaseUrl = "http://test.com", ProviderType = ProviderType.OpenAI, AuthenticationType = AuthenticationType.ApiKey };
    _providerConfigurationServiceMock
        .Setup(s => s.CreateProviderConfigurationAsync(dto))
        .ReturnsAsync(createdConfig);

    // Act
    var result = await _controller.CreateProviderConfiguration(dto);

    // Assert
    var createdResult = Assert.IsType<CreatedAtActionResult>(result);
    Assert.Equal(nameof(_controller.GetProviderConfiguration), createdResult.ActionName);
    Assert.Equal(createdConfig.Id, createdResult.RouteValues["id"]);
    var returnedDto = Assert.IsType<ProviderConfigurationDto>(createdResult.Value);
    Assert.Equal(createdConfig.Id, returnedDto.Id);
    Assert.Equal(createdConfig.Name, returnedDto.Name);
    Assert.Equal(createdConfig.BaseUrl, returnedDto.BaseUrl);
    Assert.Equal(createdConfig.ProviderType, returnedDto.ProviderType);
    Assert.Equal(createdConfig.AuthenticationType, returnedDto.AuthenticationType);
}


    [Fact]
public async Task GetProviderConfiguration_ReturnsOk_WhenConfigurationExists()
{
    // Arrange
    var id = 1;
    var configDto = new ProviderConfigurationDto { Id = id, Name = "TestProvider", BaseUrl = "http://test.com", ProviderType = ProviderType.OpenAI, AuthenticationType = AuthenticationType.ApiKey };
    _providerConfigurationServiceMock
        .Setup(s => s.GetProviderConfigurationByIdAsync(id))
        .ReturnsAsync(configDto);

    // Act
    var result = await _controller.GetProviderConfiguration(id);

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    Assert.NotNull(okResult.Value);
    Assert.IsType<ProviderConfigurationDto>(okResult.Value);
    var returnedDto = (ProviderConfigurationDto)okResult.Value;
    Assert.Equal(configDto.Id, returnedDto.Id);
    Assert.Equal(configDto.Name, returnedDto.Name);
    Assert.Equal(configDto.BaseUrl, returnedDto.BaseUrl);
    Assert.Equal(configDto.ProviderType, returnedDto.ProviderType);
    Assert.Equal(configDto.AuthenticationType, returnedDto.AuthenticationType);
}

[Fact]
public async Task GetProviderConfiguration_ReturnsNotFound_WhenConfigurationDoesNotExist()
{
    // Arrange
    var id = 1;
    _providerConfigurationServiceMock
        .Setup(s => s.GetProviderConfigurationByIdAsync(id))
        .ReturnsAsync((ProviderConfigurationDto)null);

    // Act
    
    var result = await _controller.GetProviderConfiguration(id);

    // Assert
    Assert.IsType<NotFoundResult>(result);
}

[Fact]
public async Task GetAllProviderConfigurations_ReturnsOk_WithListOfConfigurations()
{
    // Arrange
    var configsDto = new List<ProviderConfigurationDto>
    {
        new ProviderConfigurationDto { Id = 1, Name = "TestProvider1", BaseUrl = "http://test1.com", ProviderType = ProviderType.OpenAI, AuthenticationType = AuthenticationType.ApiKey },
        new ProviderConfigurationDto { Id = 2, Name = "TestProvider2", BaseUrl = "http://test2.com", ProviderType = ProviderType.AzureOpenAI, AuthenticationType = AuthenticationType.AzureAad }
    };
    _providerConfigurationServiceMock
        .Setup(s => s.GetAllProviderConfigurationsAsync())
        .ReturnsAsync(configsDto);

    // Act
    var result = await _controller.GetAllProviderConfigurations();

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    Assert.NotNull(okResult.Value);
    Assert.IsType<List<ProviderConfigurationDto>>(okResult.Value);
    var returnedList = (List<ProviderConfigurationDto>)okResult.Value;
    Assert.Equal(configsDto.Count, returnedList.Count);
    for (int i = 0; i < configsDto.Count; i++)
    {
        Assert.Equal(configsDto[i].Id, returnedList[i].Id);
        Assert.Equal(configsDto[i].Name, returnedList[i].Name);
        Assert.Equal(configsDto[i].BaseUrl, returnedList[i].BaseUrl);
        Assert.Equal(configsDto[i].ProviderType, returnedList[i].ProviderType);
        Assert.Equal(configsDto[i].AuthenticationType, returnedList[i].AuthenticationType);
    }
}

[Fact]
public async Task GetAllProviderConfigurations_ReturnsOk_WithEmptyList_WhenNoConfigurationsExist()
{
    // Arrange
    IEnumerable<ProviderConfigurationDto> providerConfigurationsDto = new List<ProviderConfigurationDto>();
    _providerConfigurationServiceMock
        .Setup(s => s.GetAllProviderConfigurationsAsync())
        .ReturnsAsync(providerConfigurationsDto);

    // Act
    var result = await _controller.GetAllProviderConfigurations();

    // Assert
    var okResult = Assert.IsType<OkObjectResult>(result);
    Assert.NotNull(okResult.Value);
    Assert.Empty((IEnumerable<ProviderConfigurationDto>)okResult.Value);
}
}