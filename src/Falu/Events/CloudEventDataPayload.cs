namespace Falu.Events;

/// <summary>
/// The payload associated with a webhook event via cloud events
/// </summary>
/// <typeparam name="TObject"></typeparam>
public class CloudEventDataPayload<TObject> : WebhookEventData<TObject>
{
    /// <summary>
    /// Information on the API request that instigated the event.
    /// </summary>
    public WebhookEventRequest? Request { get; set; }
}
