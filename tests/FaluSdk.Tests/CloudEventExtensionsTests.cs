using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Extensions;
using CloudNative.CloudEvents.Http;
using CloudNative.CloudEvents.SystemTextJson;
using Falu.Core;
using Falu.Events;
using Falu.MessageTemplates;
using Falu.Webhooks;
using System.Net.Http.Headers;
using Xunit;

namespace Falu.Tests;

public class CloudEventExtensionsTests
{
    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task ToFaluWebhookEvent_Works(bool live)
    {
        var template = new MessageTemplate
        {
            Alias = "school-promo-2022",
            Body = "You are invited to the Teacher-Parent meeting on {{date}}.",
            Id = "mtpl_1234567890",
            WorkspaceId = "wksp_1234567890",
            Live = live,
        };

        var time = DateTimeOffset.UtcNow.AddHours(-3);
        var recoded = await RecodeAsync(template, EventTypes.MessageTemplateCreated, time);
        Assert.NotNull(recoded);
        Assert.Equal(template.WorkspaceId, recoded.GetWorkspace());
        Assert.Equal(template.Live, recoded.GetLiveMode());
        Assert.Equal("application/json", recoded.DataContentType);
        Assert.Equal($"io.falu.{EventTypes.MessageTemplateCreated}", recoded.Type);
        Assert.Equal(new Uri($"https://dashboard.falu.io/{template.WorkspaceId}/events/evt_1234567890"), recoded.Source);
        Assert.Equal("evt_1234567890", recoded.Id);

        var evt = recoded.ToFaluWebhookEvent<MessageTemplate>();
        Assert.NotNull(evt);
        Assert.NotNull(evt!.Data);
        Assert.NotNull(evt.Data!.Object);
        Assert.Equal("mtpl_1234567890", evt.Data.Object!.Id);
        Assert.Equal("school-promo-2022", evt.Data.Object!.Alias);
        Assert.Equal("You are invited to the Teacher-Parent meeting on {{date}}.", evt.Data.Object!.Body);
        Assert.Equal(template.WorkspaceId, evt.Data.Object!.WorkspaceId);
        Assert.Equal(template.Live, evt.Data.Object!.Live);
    }

    private static async Task<CloudEvent> RecodeAsync<T>(T data, string type, DateTimeOffset time) where T : class, IHasWorkspace, IHasLive
    {
        var eventId = "evt_1234567890";
        var source = new Uri($"https://dashboard.falu.io/{data.WorkspaceId}/events/{eventId}");
        var cloudEvent = new CloudEvent
        {
            Id = eventId,
            Time = time,
            Source = source,
            Type = $"io.falu.{type}",
            DataContentType = "application/json",
            Data = new CloudEventExtensions.CloudEventDataPayload<T>
            {
                Object = data,
                Previous = null,
                Request = new WebhookEventRequest
                {
                    Id = "req_1234567890",
                    IdempotencyKey = null,
                },
            },
        };

        cloudEvent[CloudNative.CloudEvents.Extensions.Falu.WorkspaceAttribute] = data.WorkspaceId;
        cloudEvent[CloudNative.CloudEvents.Extensions.Falu.LiveModeAttribute] = data.Live;

        var formatter = new JsonEventFormatter();
        var content = cloudEvent.ToHttpContent(ContentMode.Structured, formatter);
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StreamContent(await content.ReadAsStreamAsync())
        };
        foreach (var h in content.Headers)
        {
            HttpHeaders headers = h.Key.StartsWith("Content-") ? response.Content.Headers : response.Headers;
            headers.Add(h.Key, h.Value);
        }

        return await response.ToCloudEventAsync(formatter, CloudNative.CloudEvents.Extensions.Falu.AllAttributes);
    }
}
