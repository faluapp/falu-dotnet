using Falu.Core;

namespace Falu.Webhooks;

/// <summary>
/// Represents a webhook endpoint.
/// </summary>
public class WebhookEndpoint : IHasId, IHasCreated, IHasUpdated, IHasDescription, IHasMetadata, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// The full value is only returned at creation.
    /// Otherwise, a secured value is returned.
    /// For example: <c>e0gNHBa90CfdKbtcWgksn52cvXoXMqCTaLdttJAsQVU=</c> would be
    /// returned in full on creation. However, subsequent times like read/get/update,
    /// would return either <see langword="null"/> or <c>e0gNHBa90***********************************</c>.
    /// </summary>
    public string? Secret { get; set; }

    /// <summary>
    /// The list of events to enable for this endpoint.
    /// Possible values are available in <see cref="EventTypes"/>.
    /// </summary>
    public List<string>? Events { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <summary>
    /// The status of the webhook.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// The URL of the webhook endpoint
    /// </summary>
    public string? Url { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
