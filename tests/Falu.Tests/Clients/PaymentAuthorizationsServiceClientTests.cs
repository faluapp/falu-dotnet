using Falu.Core;
using Falu.PaymentAuthorizations;
using Falu.Serialization;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using Xunit;

namespace Falu.Tests.Clients;

public class PaymentAuthorizationsServiceClientTests : BaseServiceClientTests<PaymentAuthorization>
{
    public PaymentAuthorizationsServiceClientTests() : base(new()
    {
        Id = "pauth_123",
        Currency = "KES",
        Amount = 5_000_00,
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
        Mpesa = new Payments.PaymentMpesaDetails
        {
            Phone = "+254722000000",
            Reference = "MDX678TSQ",
            BusinessShortCode = "5001"
        },
    }, "/v1/payment_authorizations")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.PaymentAuthorizations.GetAsync(Data!.Id!, requestOptions);
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
            var opt = new PaymentAuthorizationsListOptions
            {
                Count = 1
            };

            var response = await client.PaymentAuthorizations.ListAsync(opt, requestOptions);

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
            var opt = new PaymentAuthorizationsListOptions
            {
                Count = 1
            };

            var results = new List<PaymentAuthorization>();

            await foreach (var item in client.PaymentAuthorizations.ListRecursivelyAsync(opt, requestOptions))
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
    public async Task ApproveAsync_Works(RequestOptions requestOptions)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id}/approve", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, requestOptions);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(Data!, FaluSerializerContext.Default.PaymentAuthorization),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new PaymentAuthorizationApproveOptions { };
            var response = await client.PaymentAuthorizations.ApproveAsync(Data!.Id!, model, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task DeclineAsync_Works(RequestOptions requestOptions)
    {
        var handler = new DynamicHttpMessageHandler((req, ct) =>
        {
            Assert.Equal(HttpMethod.Post, req.Method);
            Assert.Equal($"{BasePath}/{Data!.Id}/decline", req.RequestUri!.AbsolutePath);

            AssertRequestHeaders(req, requestOptions);

            var response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(
                    JsonSerializer.Serialize(Data!, FaluSerializerContext.Default.PaymentAuthorization),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json)
            };

            return response;
        });

        await TestAsync(handler, async (client) =>
        {
            var model = new PaymentAuthorizationDeclineOptions { };
            var response = await client.PaymentAuthorizations.DeclineAsync(Data!.Id!, model, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task UpdateAsync_Works(RequestOptions requestOptions)
    {
        var handler = UpdateAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var options = new PaymentAuthorizationUpdateOptions
            {
                Metadata = new Dictionary<string, string> { ["reason"] = "loan-repayment" }
            };
            var response = await client.PaymentAuthorizations.UpdateAsync(Data!.Id!, options, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
