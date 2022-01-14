namespace Falu.Events;

/// <summary>
/// The data associated with a webhook event
/// </summary>
public class WebhookEventData<TObject>
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
}
