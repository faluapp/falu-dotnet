using Falu.Core;
using Falu.Identity;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mime;
using System.Text;
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
        DocumentType = IdentityDocumentKind.NationalId,
        DocumentNumber = "123",
        Name = "cake",
        Birthday = DateTimeOffset.UtcNow.AddYears(-25),
        Gender = Gender.Male
    };

    private readonly MarketingResult marketing = new()
    {
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        Country = "ken",
        Phones = new List<string> { "+254722000000", "+255722000000" }
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
                Content = new StringContent(JsonConvert.SerializeObject(identity), Encoding.UTF8, MediaTypeNames.Application.Json)
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

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task MarketingAsync_Works(RequestOptions options)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/marketing", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, options);

            var content = new List<MarketingResult> { marketing };
            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, MediaTypeNames.Application.Json)
            };
        });

        await TestAsync(handler, async (client) =>
        {
            var marketing_options = new MarketingListOptions
            {
                Age = new RangeFilteringOptions<int>
                {
                    GreaterThan = 24,
                    LessThanOrEqualTo = 30
                },
                Gender = Gender.Male
            };

            var response = await client.Identity.MarketingAsync(marketing_options, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Single(response.Resource);
        });
    }

}
