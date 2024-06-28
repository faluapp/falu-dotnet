using Falu.Core;
using Falu.TemporaryKeys;
using System.Net;
using Xunit;

namespace Falu.Tests.Clients;

public class TemporaryKeysServiceClientTests : BaseServiceClientTests<TemporaryKey>
{
    public TemporaryKeysServiceClientTests() : base(new()
    {
        Id = "key_123",
        Objects = ["idv_1234567890",],
        Secret = "ftkt_1234567890",
        Created = DateTimeOffset.UtcNow,
        Expires = DateTimeOffset.UtcNow.AddHours(1),
    }, "/v1/temporary_keys")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.TemporaryKeys.GetAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id, response.Resource!.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions options, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new TemporaryKeysListOptions
            {
                Count = 1
            };

            var response = await client.TemporaryKeys.ListAsync(opt, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Single(response.Resource);

            if (hasContinuationToken) Assert.NotNull(response.ContinuationToken);
            else Assert.Null(response.ContinuationToken);

            var ev = response!.Resource!.Single();

            Assert.Equal(Data!.Id, ev.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions options)
    {
        var handler = ListAsync_Handler(options: options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new TemporaryKeysListOptions
            {
                Count = 1
            };

            var results = new List<TemporaryKey>();

            await foreach (var item in client.TemporaryKeys.ListRecursivelyAsync(opt, options))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var ev = results.Single();
            Assert.Equal(Data!.Id, ev.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions options)
    {
        var handler = CreateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var model = new TemporaryKeyCreateOptions
            {
                IdentityVerification = "idv_1234567890",
            };

            var response = await client.TemporaryKeys.CreateAsync(model, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task DeleteAsync_Works(RequestOptions options)
    {
        var handler = DeleteAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.TemporaryKeys.DeleteAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
