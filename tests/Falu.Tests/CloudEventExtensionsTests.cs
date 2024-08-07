﻿using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Extensions;
using CloudNative.CloudEvents.SystemTextJson;
using Falu.Payments;
using System.Net.Mime;
using Xunit;

namespace Falu.Tests;

public class CloudEventExtensionsTests
{
    [Fact]
    public async Task ToFaluWebhookEvent_Works()
    {
        var stream = TestSamples.GetCloudEventAsStreamAsync();
        var formatter = new JsonEventFormatter();
        var cloudEvent = await formatter.DecodeStructuredModeMessageAsync(
            body: stream,
            contentType: new ContentType("application/json"),
            extensionAttributes: CloudNative.CloudEvents.Extensions.Falu.AllAttributes);
        Assert.NotNull(cloudEvent);
        Assert.Equal("wksp_602", cloudEvent.GetWorkspace());
        Assert.Equal(false, cloudEvent.GetLiveMode());
        Assert.Equal("application/json", cloudEvent.DataContentType);
        Assert.Equal("io.falu.money_balances.updated", cloudEvent.Type);
        Assert.Equal(new Uri("https://dashboard.falu.io/wksp_602/events/evt_602"), cloudEvent.Source);
        Assert.Equal("evt_602", cloudEvent.Id);

        var evt = cloudEvent.ToFaluWebhookEvent<MoneyBalances>();
        Assert.NotNull(evt);

        Assert.Equal("wksp_602", evt!.Workspace);
        Assert.False(evt.Live);

        Assert.Equal("req_602", evt.Request!.Id);
        Assert.Equal("idempotency-key-123", evt.Request.IdempotencyKey);

        Assert.NotNull(evt.Data);
        Assert.NotNull(evt.Data!.Object);
        Assert.Equal("wksp_602", evt.Data.Object!.Workspace);
        Assert.False(evt.Data.Object.Live);
        Assert.Equal("AAAAAAAAAAA=", evt.Data.Object!.Etag);
        Assert.NotNull(evt.Data.Object.Mpesa);
        Assert.Equal<string>(["123456", "654321"], evt.Data.Object.Mpesa!.Keys);
        Assert.Equal<long>([1030890L, 300500], evt.Data.Object.Mpesa!.Values);
    }
}
