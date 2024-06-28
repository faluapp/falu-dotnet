using Falu.Core;
using Falu.MessageSuppressions;
using System.Net;
using Xunit;

namespace Falu.Tests.Clients;

public class MessageSuppressionsServiceClientTests : BaseServiceClientTests<MessageSuppression>
{
    public MessageSuppressionsServiceClientTests() : base(new()
    {
        Id = "msup_123",
        Origin = "recipient",
        Reason = "spam_complain",
        Stream = "mstr_123",
        To = "+254722000000",
        Created = DateTimeOffset.UtcNow,
        Updated = DateTimeOffset.UtcNow,
    }, "/v1/message_suppressions")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageSuppressions.GetAsync(Data!.Id!, requestOptions);
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
            var opt = new MessageSuppressionsListOptions
            {
                Count = 1
            };

            var response = await client.MessageSuppressions.ListAsync(opt, requestOptions);

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
            var opt = new MessageSuppressionsListOptions
            {
                Count = 1
            };

            var results = new List<MessageSuppression>();

            await foreach (var item in client.MessageSuppressions.ListRecursivelyAsync(opt, requestOptions))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var sup = results.Single();
            Assert.Equal(Data!.Id, sup.Id);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions requestOptions)
    {
        var handler = CreateAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageSuppressionCreateOptions
            {
                Stream = "mstr_123",
                To = "+254722000000",
            };

            var response = await client.MessageSuppressions.CreateAsync(model, requestOptions);

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
            var response = await client.MessageSuppressions.DeleteAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
