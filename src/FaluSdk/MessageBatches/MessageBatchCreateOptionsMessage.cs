using Falu.Messages;

namespace Falu.MessageBatches;

/// <summary>
/// Information about one or more messages to be created in a batch
/// </summary>
public class MessageBatchCreateOptionsMessage
{
    /// <summary>
    /// Destination phone numbers in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// You can send up to 1,000 messages in one API request.
    /// </summary>
    public IList<string>? Tos { get; set; }

    /// <summary>
    /// Actual message content to be sent.
    /// Required if <see cref="Template"/> is not specified.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// The template to use.
    /// Required if <see cref="Body"/> is not specified.
    /// </summary>
    public MessageCreateOptionsTemplate? Template { get; set; }

    /// <summary>
    /// Media to be sent with the message.
    /// </summary>
    public IList<MessageCreateOptionsMedia>? Media { get; set; }
}
