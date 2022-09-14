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
    [MemberData(nameof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.MessageSuppressions.GetAsync(Data!.Id!, options);
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
            var opt = new MessageSuppressionsListOptions
            {
                Count = 1
            };

            var response = await client.MessageSuppressions.ListAsync(opt, options);

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
            var opt = new MessageSuppressionsListOptions
            {
                Count = 1
            };

            var results = new List<MessageSuppression>();

            await foreach (var item in client.MessageSuppressions.ListRecursivelyAsync(opt, options))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var msgtmpl = results.Single();
            Assert.Equal(Data!.Id, msgtmpl.Id);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task CreateAsync_Works(RequestOptions options)
    {
        var handler = CreateAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var model = new MessageSuppressionCreateRequest
            {
                Stream = "mstr_123",
                To = "+254722000000",
            };

            var response = await client.MessageSuppressions.CreateAsync(model, options);

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
            var response = await client.MessageSuppressions.DeleteAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        });
    }
}
