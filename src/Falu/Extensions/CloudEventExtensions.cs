﻿using CloudNative.CloudEvents.Extensions;
using Falu.Events;
using Falu.Serialization;
using Falu.Webhooks;
using System.Text.Json;

namespace CloudNative.CloudEvents;

/// <summary>
/// Extensions for <see cref="CloudEvent"/> relating to <see cref="WebhookEvent"/> and <see cref="WebhookEvent{TObject}"/>
/// </summary>
public static partial class CloudEventExtensions
{
    private const string TypeFormat = "^io.falu.(.*)$";
    private static readonly System.Text.RegularExpressions.Regex typeFormat = GetTypeFormat();

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
    /// This method doesn't verify <a href="https://falu.io/docs/webhooks/signatures">webhook signatures</a>.
    /// Use <see cref="WebhookUtility.ValidateSignature(byte[], string, string, TimeSpan?, DateTimeOffset?)"/> for validation.
    /// </remarks>
    public static WebhookEvent<T>? ToFaluWebhookEvent<T>(this CloudEvent @event)
    {
        if (@event is null) throw new ArgumentNullException(nameof(@event));

        var data = @event.Data;
        if (data is not JsonElement je)
        {
            throw new InvalidOperationException($"Event data of type '{data?.GetType().FullName}' cannot be parsed.");
        }

        // extract the event type
        var type = @event.Type;
        if (type is not null)
        {
            var match = typeFormat.Match(type);
            type = match.Success
                ? match.Groups[1].Value
                : throw new InvalidOperationException($"The '{nameof(@event)}.{nameof(@event.Type)}' value must start with 'io.falu.'");
        }

        if (JsonSerializer.Deserialize(je.GetRawText(), typeof(CloudEventDataPayload<T>), FaluSerializerContext.Default) is not CloudEventDataPayload<T> payload)
        {
            throw new InvalidOperationException("JSON deserialization resulted in null");
        }

        return new WebhookEvent<T>
        {
            Id = @event.Id,
            Created = @event.Time ?? DateTimeOffset.MinValue,
            Type = type,
            Data = new WebhookEventData<T>
            {
                Object = payload.Object,
                Previous = payload.Previous,
            },
            Request = payload.Request,
            Workspace = @event.GetWorkspace(),
            Live = @event.GetLiveMode() ?? false,
        };
    }

#if NET7_0_OR_GREATER
    [System.Text.RegularExpressions.GeneratedRegex(TypeFormat)]
    private static partial System.Text.RegularExpressions.Regex GetTypeFormat();
#else
    private static System.Text.RegularExpressions.Regex GetTypeFormat() => new(TypeFormat);
#endif
}
