using Falu.Events;
using System.Text;

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
    public static WebhookEvent<T>? ToFaluWebhookEvent<T>(CloudEvent @event)
    {
        var data = @event.Data;
        return data switch
        {
            string json => EventUtility.ParseEvent<T>(json),
            byte[] raw => EventUtility.ParseEvent<T>(Encoding.UTF8.GetString(raw)),
            _ => throw new InvalidOperationException($"Event data of type '{data?.GetType().FullName}' cannot be parsed."),
        };
    }
}
