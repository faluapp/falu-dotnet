using Falu.Core;
using Falu.IdentityVerificationReports;
using System.Net;
using Xunit;

namespace Falu.Tests.Clients;

public class IdentityVerificationReportsServiceClientTests : BaseServiceClientTests<IdentityVerificationReport>
{
    public IdentityVerificationReportsServiceClientTests() : base(new()
    {
        Id = "idvr_123",
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
    }, "/v1/identity/verification_reports")
    { }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.IdentityVerificationReports.GetAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id, response.Resource!.Id);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions options, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new IdentityVerificationReportsListOptions
            {
                Count = 1
            };

            var response = await client.IdentityVerificationReports.ListAsync(opt, options);

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
    [MemberData(nameof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions options)
    {
        var handler = ListAsync_Handler(options: options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new IdentityVerificationReportsListOptions
            {
                Count = 1
            };

            var results = new List<IdentityVerificationReport>();

            await foreach (var item in client.IdentityVerificationReports.ListRecursivelyAsync(opt, options))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var ev = results.Single();
            Assert.Equal(Data!.Id, ev.Id);
        });
    }
}
