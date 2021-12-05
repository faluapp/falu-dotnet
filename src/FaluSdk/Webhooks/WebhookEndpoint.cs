using Falu.Core;

namespace Falu.Webhooks;

/// <summary>
/// The basic implementation of a Webhook irrespective of the usage
/// </summary>
public class WebhookEndpoint : WebhookEndpointPatchModel, IHasId, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
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
    /// would return either <c>null</c> or <c>e0gNHBa90***********************************</c>.
    /// </summary>
    public string? Secret { get; set; }

    /// <inheritdoc/>
    public string? WorkspaceId { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
