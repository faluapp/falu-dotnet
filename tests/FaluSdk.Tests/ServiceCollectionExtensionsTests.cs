using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Xunit;

namespace Falu.Tests;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void TestAddFaluWithoutApiKey()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddFalu(options => { });
        var provider = services.BuildServiceProvider();

        // Act && Assert
        Assert.Throws<OptionsValidationException>(() => provider.GetRequiredService<FaluClient>());
    }

    [Fact]
    public void TestAddFaluReturnsServiceCollection()
    {
        // Arrange
        var collection = new ServiceCollection();

        // Act
        var builder = collection.AddFalu(options => options.ApiKey = "FAKE_APIKEY");

        // Assert
        Assert.NotNull(builder);
        Assert.IsAssignableFrom<IHttpClientBuilder>(builder);
    }

    [Fact]
    public void TestAddFaluRegisteredWithTransientLifeTime()
    {
        // Arrange
        var collection = new ServiceCollection();

        // Act
        var builder = collection.AddFalu(options => options.ApiKey = "FAKE_APIKEY");

        // Assert
        var serviceDescriptor = collection.FirstOrDefault(x => x.ServiceType == typeof(FaluClient));
        Assert.NotNull(serviceDescriptor);
        Assert.Equal(ServiceLifetime.Transient, serviceDescriptor!.Lifetime);
    }

    [Fact]
    public void TestAddFaluCanResolveFaluClient()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddFalu(options => options.ApiKey = "FAKE_APIKEY");
        var provider = services.BuildServiceProvider();

        // Act
        var client = provider.GetService<FaluClient>();

        // Assert
        Assert.NotNull(client);
    }

    [Fact]
    public void TestFaluClientOptionsAreTheSame()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddFalu(options => options.ApiKey = "FAKE_APIKEY");
        var provider = services.BuildServiceProvider();

        // Act
        var options1 = provider.GetService<IOptionsSnapshot<FaluClientOptions>>();
        var options2 = provider.GetService<IOptionsSnapshot<FaluClientOptions>>();

        // Assert
        Assert.Equal(options1, options2);
    }

    [Fact]
    public void TestFaluClientOptionsAreDifferent()
    {
        // Arrange
        var services = new ServiceCollection();
        services.AddFalu(options => options.ApiKey = "FAKE_APIKEY");
        var provider = services.BuildServiceProvider();

        // Act
        using var scope1 = provider.CreateScope();
        var provider1 = scope1.ServiceProvider;
        var options1 = provider1.GetService<IOptionsSnapshot<FaluClientOptions>>();
        using var scope2 = provider.CreateScope();
        var provider2 = scope2.ServiceProvider;
        var options2 = provider2.GetService<IOptionsSnapshot<FaluClientOptions>>();

        // Assert
        Assert.NotEqual(options1, options2);
    }
}
