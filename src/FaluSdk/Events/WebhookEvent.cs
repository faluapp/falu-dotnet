using Falu.Core;

namespace Falu.Events;

/// <summary>
/// The basic implementation of a Webhook Event irrespective of the usage
/// </summary>
public class WebhookEvent<TObject> : IHasId, IHasCreated, IHasWorkspaceId, IHasLive
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Type of event (e.g. payment.updated, money_balances.updated, etc.).
    /// Possible values are available in <see cref="Webhooks.EventTypes"/>.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Information on the API request that instigated the event.
    /// </summary>
    public WebhookEventRequest? Request { get; set; }

    /// <summary>
    /// Object containing data associated with the event.
    /// </summary>
    public WebhookEventData<TObject>? Data { get; set; }

    /// <inheritdoc/>
    public string? WorkspaceId { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }
}

/// <summary>
/// The basic implementation of a Webhook Event irrespective of the usage
/// </summary>
public class WebhookEvent : WebhookEvent<object> { }
