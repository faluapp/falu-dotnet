using Falu.Core;
using Falu.Events;
using System.Net;
using Xunit;

namespace Falu.Tests.Clients;

public class EventsServiceClientTests : BaseServiceClientTests<WebhookEvent>
{
    public EventsServiceClientTests() : base(new()
    {
        Id = "evt_123",
        Created = DateTimeOffset.UtcNow,
        Type = Webhooks.EventTypes.TransferSucceeded,
        Request = new WebhookEventRequest
        {
            IdempotencyKey = "my-key", // this is set to ensure snake_case works
        },
    }, "/v1/events")
    { }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions options)
    {
        var handler = GetAsync_Handler(options);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Events.GetAsync(Data!.Id!, options);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id, response.Resource!.Id);
            Assert.Equal(Data!.Request!.IdempotencyKey, response.Resource!.Request!.IdempotencyKey);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions options, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new EventsListOptions
            {
                Count = 1
            };

            var response = await client.Events.ListAsync(opt, options);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Single(response.Resource);

            if (hasContinuationToken) Assert.NotNull(response.ContinuationToken);
            else Assert.Null(response.ContinuationToken);

            var ev = response!.Resource!.Single();

            Assert.Equal(Data!.Id, ev.Id);
            Assert.Equal(Data!.Request!.IdempotencyKey, ev.Request!.IdempotencyKey);
        });
    }

    [Theory]
    [MemberData(nameof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions options)
    {
        var handler = ListAsync_Handler(options: options);

        await TestAsync(handler, async (client) =>
        {
            var opt = new EventsListOptions
            {
                Count = 1
            };

            var results = new List<WebhookEvent>();

            await foreach (var item in client.Events.ListRecursivelyAsync(opt, options))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var ev = results.Single();
            Assert.Equal(Data!.Id, ev.Id);
        });
    }
}
