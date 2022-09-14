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
        Objects = new List<string> { "idv_1234567890", },
        Secret = "ftkt_1234567890",
        Created = DateTimeOffset.UtcNow,
        Expires = DateTimeOffset.UtcNow.AddHours(1),
    }, "/v1/temporary_keys")
    { }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions options)
    {
        var handler = CreateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var model = new TemporaryKeyCreateRequest
            {
                IdentityVerification = "idv_1234567890",
            };

            var response = await client.TemporaryKeys.CreateAsync(model, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
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
