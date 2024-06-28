using Falu.Core;
using Falu.Payments;
using System.Net;
using Xunit;

namespace Falu.Tests.Clients;

public class PaymentsServiceClientTests : BaseServiceClientTests<Payment>
{
    public PaymentsServiceClientTests() : base(new()
    {
        Id = "pa_123",
        Currency = "kes",
        Amount = 100_00,
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
    }, "/v1/payments")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Payments.GetAsync(Data!.Id!, requestOptions);
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
            var opt = new PaymentsListOptions
            {
                Count = 1
            };

            var response = await client.Payments.ListAsync(opt, requestOptions);

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
            var opt = new PaymentsListOptions
            {
                Count = 1
            };

            var results = new List<Payment>();

            await foreach (var item in client.Payments.ListRecursivelyAsync(opt, requestOptions))
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
            var model = new PaymentCreateOptions
            {
                Currency = Data!.Currency,
                Amount = Data!.Amount,
                Mpesa = new PaymentCreateOptionsMpesa
                {
                    Phone = "+254722000000",
                    Paybill = true,
                    Reference = "5001"
                }
            };

            var response = await client.Payments.CreateAsync(model, requestOptions);

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
            var options = new PaymentUpdateOptions
            {
                Description = "new description"
            };
            var response = await client.Payments.UpdateAsync(Data!.Id!, options, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
        });
    }
}
