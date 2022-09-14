using Falu.Core;

namespace Falu.Messages;

/// <summary>
/// A message record.
/// </summary>
public class Message : MessagePatchModel, IHasId, IHasCreated, IHasUpdated, IHasRedaction, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Status of the message.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Destination phone number in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// </summary>
    public string? To { get; set; }

    /// <summary>
    /// Gets or sets the contents of the message.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Template used for the message.
    /// </summary>
    public MessageSourceTemplate? Template { get; set; }

    /// <summary>
    /// Stream used for the message.
    /// </summary>
    public string? Stream { get; set; }

    /// <summary>
    /// Batch that the message belogs to, if any.
    /// </summary>
    public string? Batch { get; set; }

    /// <summary>
    /// Identifier of the Customer this Message belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

    /// <summary>
    /// Number of segments that make up the complete message.
    /// If the body that is too large to be sent in a single
    /// message, it is segmented and charged as multiple messages.
    /// <br/>
    /// Inbound messages over 160 characters are reassembled when the message is received.
    /// </summary>
    public int Segements { get; set; }

    /// <summary>
    /// Schedule information for the message.
    /// </summary>
    public MessageSchedule? Schedule { get; set; }

    /// <summary>
    /// Time at which the message was sent.
    /// </summary>
    public DateTimeOffset? Sent { get; set; }

    /// <summary>
    /// If present, this property tells you the error encountered when processing the message.
    /// </summary>
    public MessageError? Error { get; set; }

    /// <summary>
    /// Time at which the message was delivered.
    /// This is dependent on the underlying provider.
    /// </summary>
    public DateTimeOffset? Delivered { get; set; }

    /// <inheritdoc/>
    public DataRedaction? Redaction { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
