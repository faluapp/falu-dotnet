using CloudNative.CloudEvents.Extensions;
using Falu;
using Falu.Events;
using System.Text.Json;

namespace CloudNative.CloudEvents;

/// <summary>
/// Extensions for <see cref="CloudEvent"/> relating to <see cref="WebhookEvent"/> and <see cref="WebhookEvent{TObject}"/>
/// </summary>
public static class CloudEventExtensions
{
    /// <summary>
    /// Convert a <see cref="CloudEvent"/> to a <see cref="WebhookEvent{TObject}"/> object.
    /// <br/>
    /// When using <see cref="CloudEvent"/> to receive webhooks, it is recommended to use the
    /// <see href="https://www.nuget.org/packages/CloudNative.CloudEvents.AspNetCore/">CloudNative.CloudEvents.AspNetCore</see>
    /// package.
    /// </summary>
    /// <param name="event">The received <see cref="CloudEvent"/>.</param>
    /// <returns>The deserialized <see cref="WebhookEvent{TObject}"/>.</returns>
    /// <remarks>
    /// This method doesn't verify <a href="https://docs.falu.io/webhooks/signatures">webhook signatures</a>.
    /// Use <see cref="EventUtility.ValidateSignature(byte[], string, string, long?, long?)"/> for validation.
    /// </remarks>
    public static WebhookEvent<T>? ToFaluWebhookEvent<T>(this CloudEvent @event)
    {
        if (@event is null) throw new ArgumentNullException(nameof(@event));

        var data = @event.Data;
        if (data is not JsonElement je)
        {
            throw new InvalidOperationException($"Event data of type '{data?.GetType().FullName}' cannot be parsed.");
        }

        var options = FaluClientOptions.CreateSerializerOptions();
        var ce_payload = JsonSerializer.Deserialize<CloudEventDataPayload<T>>(je.GetRawText(), options);
        if (ce_payload is null)
        {
            throw new InvalidOperationException("JSON deserialization resulted in null");
        }

        return new WebhookEvent<T>
        {
            Created = @event.Time ?? DateTimeOffset.MinValue,
            Data = new WebhookEventData<T>
            {
                Object = ce_payload.Object,
                Previous = ce_payload.Previous,
            },
            Id = @event.Id,
            Request = ce_payload.Request,
            Type = @event.Type,
            WorkspaceId = @event.GetWorkspace(),
            Live = @event.GetLiveMode() ?? false,
        };
    }

    internal class CloudEventDataPayload<TObject>
    {
        /// <summary>
        /// Object containing the API resource relevant to the event.
        /// For example, a <c>money_balances.updated</c> event will have a full balance object.
        /// </summary>
        public TObject? Object { get; set; }

        /// <summary>
        /// Object containing the names of the properties that have changed, and their previous
        /// values (sent along only with <c>*.updated</c> events).
        /// </summary>
        public TObject? Previous { get; set; }

        /// <summary>
        /// Information on the API request that instigated the event.
        /// </summary>
        public WebhookEventRequest? Request { get; set; }
    }
}
