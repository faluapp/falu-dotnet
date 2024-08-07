﻿using Falu.Core;
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
        Data = new WebhookEventData<System.Text.Json.Nodes.JsonObject>
        {
            Object = System.Text.Json.Nodes.JsonNode.Parse("{\"id\":\"tmpl_123\",\"body\":\"your code is {{otp_code}}\"}")!.AsObject(),
        },
    }, "/v1/events")
    { }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task GetAsync_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Events.GetAsync(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id, response.Resource!.Id);
            Assert.Equal(Data!.Request!.IdempotencyKey, response.Resource!.Request!.IdempotencyKey);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Works(RequestOptions requestOptions, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new EventsListOptions
            {
                Count = 1
            };

            var response = await client.Events.ListAsync(opt, requestOptions);

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
    [ClassData(typeof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Works(RequestOptions requestOptions)
    {
        var handler = ListAsync_Handler(requestOptions: requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new EventsListOptions
            {
                Count = 1
            };

            var results = new List<WebhookEvent>();

            await foreach (var item in client.Events.ListRecursivelyAsync(opt, requestOptions))
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
    public async Task GetAsync_Generic_Works(RequestOptions requestOptions)
    {
        var handler = GetAsync_Handler(requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var response = await client.Events.GetAsync<MessageTemplates.MessageTemplate>(Data!.Id!, requestOptions);
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Equal(Data!.Id, response.Resource!.Id);
            Assert.Equal(Data!.Request!.IdempotencyKey, response.Resource!.Request!.IdempotencyKey);
            Assert.Equal("tmpl_123", response.Resource!.Data!.Object!.Id);
            Assert.Equal("your code is {{otp_code}}", response.Resource!.Data!.Object.Body);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsWithHasContinuationTokenData))]
    public async Task ListAsync_Generic_Works(RequestOptions requestOptions, bool hasContinuationToken)
    {
        var handler = ListAsync_Handler(hasContinuationToken, requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new EventsListOptions
            {
                Count = 1
            };

            var response = await client.Events.ListAsync<MessageTemplates.MessageTemplate>(opt, requestOptions);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Resource);
            Assert.Single(response.Resource);

            if (hasContinuationToken) Assert.NotNull(response.ContinuationToken);
            else Assert.Null(response.ContinuationToken);

            var ev = response!.Resource!.Single();

            Assert.Equal(Data!.Id, ev.Id);
            Assert.Equal(Data!.Request!.IdempotencyKey, ev.Request!.IdempotencyKey);
            Assert.Equal("tmpl_123", ev.Data!.Object!.Id);
            Assert.Equal("your code is {{otp_code}}", ev.Data!.Object.Body);
        });
    }

    [Theory]
    [ClassData(typeof(RequestOptionsData))]
    public async Task ListRecursivelyAsync_Generic_Works(RequestOptions requestOptions)
    {
        var handler = ListAsync_Handler(requestOptions: requestOptions);

        await TestAsync(handler, async (client) =>
        {
            var opt = new EventsListOptions
            {
                Count = 1
            };

            var results = new List<WebhookEvent<MessageTemplates.MessageTemplate>>();

            await foreach (var item in client.Events.ListRecursivelyAsync<MessageTemplates.MessageTemplate>(opt, requestOptions))
            {
                results.Add(item);
            }

            Assert.Single(results);
            var ev = results.Single();
            Assert.Equal(Data!.Id, ev.Id);
            Assert.Equal("tmpl_123", ev.Data!.Object!.Id);
            Assert.Equal("your code is {{otp_code}}", ev.Data!.Object.Body);
        });
    }
}
