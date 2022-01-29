using Falu.Core;
using Falu.PaymentRefunds;
using System.Net;
using Tingle.Extensions.JsonPatch;
using Xunit;

namespace Falu.Tests.Clients;

public class PaymentRefundsServiceClientTests : BaseServiceClientTests<PaymentRefund>
{
    public PaymentRefundsServiceClientTests() : base(new()
    {
        Id = "pr_123",
        Payment = "pa_123",
        Currency = "KES",
        Amount = 50_000_00,
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        Mpesa = new PaymentRefundMpesaDetails
        {
            BusinessShortCode = "5001",
            RequestId = Guid.NewGuid().ToString()
        },
    }, "/v1/payment_refunds")
    { }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.PaymentRefunds.GetAsync(Data!.Id!, options);
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
            var opt = new PaymentRefundsListOptions
            {
                Count = 1
            };

            var response = await client.PaymentRefunds.ListAsync(opt, options);

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
            var opt = new PaymentRefundsListOptions
            {
                Count = 1
            };

            var results = new List<PaymentRefund>();

            await foreach (var item in client.PaymentRefunds.ListRecursivelyAsync(opt, options))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var ev = results.Single();
            Assert.Equal(Data!.Id, ev.Id);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions options)
    {
        var handler = CreateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var model = new PaymentRefundCreateRequest
            {
                Payment = Data!.Payment,
                Reason = "customer_requested",
            };

            var response = await client.PaymentRefunds.CreateAsync(model, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task UpdateAsync_Works(RequestOptions options)
    {
        var handler = UpdateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var document = new JsonPatchDocument<PaymentRefundPatchModel>();
            document.Replace(x => x.Description, "new description");

            var response = await client.PaymentRefunds.UpdateAsync(Data!.Id!, document, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
