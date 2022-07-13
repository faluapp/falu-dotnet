using Falu.Core;
using Falu.Identity;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Falu.Tests.Clients;

public class IdentityServiceClientTests : BaseServiceClientTests
{
    private const string BasePath = "/v1/identity";
    private readonly IdentityRecord identity = new()
    {
        Id = "idt_123",
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        DocumentType = "nationalId",
        DocumentNumber = "123",
        Name = "cake",
        Birthday = DateTimeOffset.UtcNow.AddYears(-25),
        Gender = "male"
    };

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task SearchAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/search", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(identity), Encoding.UTF8, MediaTypeNames.Application.Json)
            };
        });

        await TestAsync(handler, async (client) =>
        {
            var search = new IdentitySearchModel
            {
                DocumentNumber = identity.DocumentNumber,
                Country = identity.Country,
                DocumentType = identity.DocumentType
            };

            var response = await client.Identity.SearchAsync(search, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(identity.DocumentNumber, response.Resource!.DocumentNumber);
            Assert.Equal(identity.DocumentType, response.Resource!.DocumentType);
            Assert.Equal(identity.Country, response.Resource!.Country);
        });
    }
}
