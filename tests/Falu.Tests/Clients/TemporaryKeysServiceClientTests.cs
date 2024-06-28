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
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.TemporaryKeys.GetAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id, response.Resource!.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions requestOptions, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new TemporaryKeysListOptions
            {
                Count = 1
            };

            var response = await client.TemporaryKeys.ListAsync(opt, requestOptions);

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
    public async Task ListRecursivelyAsync_Works(RequestOptions requestOptions)
    {
        var handler = ListAsync_Handler(requestOptions: requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new TemporaryKeysListOptions
            {
                Count = 1
            };

            var results = new List<TemporaryKey>();

            await foreach (var item in client.TemporaryKeys.ListRecursivelyAsync(opt, requestOptions))
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
    public async Task CreateAsync_Works(RequestOptions requestOptions)
    {
        var handler = CreateAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var model = new TemporaryKeyCreateOptions
            {
                IdentityVerification = "idv_1234567890",
            };

            var response = await client.TemporaryKeys.CreateAsync(model, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task DeleteAsync_Works(RequestOptions requestOptions)
    {
        var handler = DeleteAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.TemporaryKeys.DeleteAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
