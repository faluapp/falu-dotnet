using Falu.Core;

namespace Falu.MessageTemplates;

/// <summary>
/// A template for sending messages.
/// </summary>
public class MessageTemplate : MessageTemplatePatchModel, IHasId, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <inheritdoc/>
    public string? WorkspaceId { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
