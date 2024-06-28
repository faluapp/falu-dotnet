using Falu.Messages;

namespace Falu.MessageBatches;

/// <summary>
/// Information for creating a message batch.
/// </summary>
public class MessageBatchCreateOptions
{
    /// <summary>
    /// The messages.
    /// You can send up to 1,000 messages in one API request.
    /// </summary>
    public List<MessageBatchCreateOptionsMessage>? Messages { get; set; }

    /// <summary>
    /// The stream to use.
    /// It can either be the name or unique identifier of the stream.
    /// If not provided, message will default to the "transactional" stream.
    /// </summary>
    public string Stream { get; set; } = "transactional";

    /// <summary>
    /// Schedule for sending the message batch.
    /// When <see langword="null"/>, the message batch
    /// is enqueued for immediate sending.
    /// </summary>
    public MessageCreateOptionsSchedule? Schedule { get; set; }
}
