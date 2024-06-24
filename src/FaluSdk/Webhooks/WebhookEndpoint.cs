using Falu.Core;

namespace Falu.Webhooks;

/// <summary>
/// Represents a webhook endpoint.
/// </summary>
public class WebhookEndpoint : WebhookEndpointUpdateOptions, IHasId, IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
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

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
