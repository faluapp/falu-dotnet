using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Falu.Tests
{
    public class ServiceCollectionExtensionsTests
    {
        [Fact]
        public void TestAddFaluWithoutApiKey()
        {
            // Arrange
            var services = new ServiceCollection().AddFalu(options => { }).BuildServiceProvider();

            // Act && Assert
            Assert.Throws<FaluException>(() => services.GetRequiredService<FaluClient>());
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
            Assert.IsAssignableFrom<IServiceCollection>(builder);
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
        public void TestAddFaluCanResolveFaluClientOptions()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddFalu(options => options.ApiKey = "FAKE_APIKEY")
                .BuildServiceProvider();

            // Act
            var options = services.GetService<IOptions<FaluClientOptions>>();

            // Assert
            Assert.NotNull(options);
        }

        [Fact]
        public void TestAddFaluCanResolveFaluClient()
        {
            // Arrange
            var services = new ServiceCollection()
                .AddFalu(options => options.ApiKey = "FAKE_APIKEY")
                .BuildServiceProvider();

            // Act
            var client = services.GetService<FaluClient>();

            // Assert
            Assert.NotNull(client);
        }

        [Fact]
        public void TestAddFaluCanResolveFaluClientFromConfiguration()
        {
            // Arrange
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>
                {
                    ["Falu:ApiKey"] = "FAKE_APIKEY",
                    ["Falu:Retries"] = "0",
                })
                .Build();
            var services = new ServiceCollection()
                .AddFalu(configuration.GetSection("Falu"))
                .BuildServiceProvider();

            // Act
            var client = services.GetService<FaluClient>();

            // Assert
            Assert.NotNull(client);
        }
    }
}
