using Falu.Core;

namespace Falu.Messages;

/// <summary>
/// A message record.
/// </summary>
public class Message : IHasId, IHasCreated, IHasUpdated, IHasRedaction, IHasMetadata, IHasWorkspace, IHasLive, IHasEtag
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
    /// Media included with the message.
    /// </summary>
    public List<MessageMedia>? Media { get; set; }

    /// <summary>
    /// Stream used for the message.
    /// </summary>
    public string? Stream { get; set; }

    /// <summary>
    /// Batch that the message belongs to, if any.
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
    public int? Segments { get; set; }

    /// <summary>
    /// Schedule information for the message.
    /// </summary>
    public MessageSchedule? Schedule { get; set; }

    /// <summary>
    /// Time at which the message was sent.
    /// </summary>
    public DateTimeOffset? Sent { get; set; }

    /// <summary>
    /// Details on the message error.
    /// Present when in failed state.
    /// </summary>
    public ObjectError? Error { get; set; }

    /// <summary>
    /// Time at which the message was delivered.
    /// This is dependent on the underlying provider.
    /// </summary>
    public DateTimeOffset? Delivered { get; set; }

    /// <inheritdoc/>
    public DataRedaction? Redaction { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
