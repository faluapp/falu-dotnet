using Falu.Core;

namespace Falu.MessageBatches;

/// <summary>
/// Represents a batch of messages
/// </summary>
public class MessageBatch : MessageBatchPatchModel, IHasId, IHasCreated, IHasUpdated, IHasRedaction, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Stream used for the message.
    /// </summary>
    public string? Stream { get; set; }

    /// <summary>
    /// Schedule information for the message batch.
    /// </summary>
    public MessageBatchSchedule? Schedule { get; set; }

    /// <summary>
    /// Unique identifiers of the messages tracked in this batch.
    /// </summary>
    public List<string>? Messages { get; set; }

    /// <summary>
    /// Total number of segments from each message.
    /// </summary>
    public int Segments { get; set; }

    /// <inheritdoc/>
    public DataRedaction? Redaction { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
