using CloudNative.CloudEvents;
using CloudNative.CloudEvents.Http;
using CloudNative.CloudEvents.SystemTextJson;
using Falu.Core;
using Falu.Evaluations;
using Falu.Events;
using Falu.MessageTemplates;
using Falu.Webhooks;
using System.Net.Http.Headers;
using Xunit;

namespace Falu.Tests;

public class CloudEventExtensionsTests
{
    [Fact]
    public async Task ToFaluWebhookEvent_Works_For_Binary()
    {
        var evaluation = new Evaluation
        {
            Scope = "personal",
            Statement = new Statement { Email = "test@test.com", },
            Id = "ev_1234567890",
            WorkspaceId = "wksp_1234567890",
            Live = false,
        };

        var time = DateTimeOffset.UtcNow.AddHours(-3);
        var recoded = await RecodeAsync(evaluation, EventTypes.EvaluationCompleted, time, ContentMode.Binary);
        Assert.NotNull(recoded);
        Assert.Equal("wksp_1234567890", recoded.Subject);
        Assert.Equal("application/json", recoded.DataContentType);
        Assert.Equal(EventTypes.EvaluationCompleted, recoded.Type);
        Assert.Equal(new Uri($"https://dashboard.falu.io/{evaluation.WorkspaceId}/events/evt_1234567890"), recoded.Source);
        Assert.Equal("evt_1234567890", recoded.Id);

        var evt = recoded.ToFaluWebhookEvent<Evaluation>();
        Assert.NotNull(evt);
        Assert.NotNull(evt!.Data);
        Assert.NotNull(evt.Data!.Object);
        Assert.Equal("ev_1234567890", evt.Data.Object!.Id);
        Assert.Equal("personal", evt.Data.Object!.Scope);
        Assert.Equal("test@test.com", evt.Data.Object!.Statement?.Email);
        Assert.Equal("wksp_1234567890", evt.Data.Object!.WorkspaceId);
    }

    [Fact]
    public async Task ToFaluWebhookEvent_Works_For_Structured()
    {
        var template = new MessageTemplate
        {
            Alias = "school-promo-2022",
            Body = "You are invited to the Teacher-Parent meeting on {{date}}.",
            Id = "mtpl_1234567890",
            WorkspaceId = "wksp_1234567890",
            Live = false,
        };

        var time = DateTimeOffset.UtcNow.AddHours(-3);
        var recoded = await RecodeAsync(template, EventTypes.MessageTemplateCreated, time, ContentMode.Structured);
        Assert.NotNull(recoded);
        Assert.Equal("wksp_1234567890", recoded.Subject);
        Assert.Equal("application/json", recoded.DataContentType);
        Assert.Equal(EventTypes.MessageTemplateCreated, recoded.Type);
        Assert.Equal(new Uri($"https://dashboard.falu.io/{template.WorkspaceId}/events/evt_1234567890"), recoded.Source);
        Assert.Equal("evt_1234567890", recoded.Id);

        var evt = recoded.ToFaluWebhookEvent<MessageTemplate>();
        Assert.NotNull(evt);
        Assert.NotNull(evt!.Data);
        Assert.NotNull(evt.Data!.Object);
        Assert.Equal("mtpl_1234567890", evt.Data.Object!.Id);
        Assert.Equal("school-promo-2022", evt.Data.Object!.Alias);
        Assert.Equal("You are invited to the Teacher-Parent meeting on {{date}}.", evt.Data.Object!.Body);
        Assert.Equal("wksp_1234567890", evt.Data.Object!.WorkspaceId);
    }

    private static async Task<CloudEvent> RecodeAsync<T>(T data, string type, DateTimeOffset time, ContentMode mode) where T : class, IHasWorkspaceId
    {
        var eventId = "evt_1234567890";
        var source = new Uri($"https://dashboard.falu.io/{data.WorkspaceId}/events/{eventId}");
        var cloudEvent = new CloudEvent
        {
            Id = eventId,
            Time = time,
            Source = source,
            Type = type,
            DataContentType = "application/json",
            Data = new WebhookEvent<T>
            {
                Created = time,
                Data = new WebhookEventData<T> { Object = data, Previous = null, },
                Id = eventId,
                Type = type,
                Live = false,
                WorkspaceId = data.WorkspaceId,
                Request = new WebhookEventRequest
                {
                    Id = "req_1234567890",
                    IdempotencyKey = null,
                },
            },
            Subject = data.WorkspaceId,
        };

        var formatter = new JsonEventFormatter();
        var content = cloudEvent.ToHttpContent(mode, formatter);
        var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK)
        {
            Content = new StreamContent(await content.ReadAsStreamAsync())
        };
        foreach (var h in content.Headers)
        {
            HttpHeaders headers = h.Key.StartsWith("Content-") ? response.Content.Headers : response.Headers;
            headers.Add(h.Key, h.Value);
        }

        return await response.ToCloudEventAsync(formatter);
    }
}
