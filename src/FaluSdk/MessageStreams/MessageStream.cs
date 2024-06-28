using Falu.Core;

namespace Falu.MessageStreams;

/// <summary>
/// Represents a stream used for sending messages.
/// This is a way to separate messages sent to ensure high delivery rates.
/// </summary>
public class MessageStream : MessageStreamUpdateOptions, IHasId, IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <summary>
    /// Name of the stream.
    /// </summary>
    public string? Name { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Type of stream.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Time at which the stream was archived.
    /// Only populated once a stream is archived.
    /// </summary>
    public DateTimeOffset? Archived { get; set; }

    /// <summary>
    /// Indicates if the stream is one created by default.
    /// </summary>
    public bool Default { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
