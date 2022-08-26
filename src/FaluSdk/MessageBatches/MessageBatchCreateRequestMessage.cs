using Falu.Messages;

namespace Falu.MessageBatches;

/// <summary>
/// Information about one or more messages to be created in a batch
/// </summary>
public class MessageBatchCreateRequestMessage
{
    /// <summary>
    /// Destination phone numbers in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// You can provide up to 500 numbers in one request.
    /// </summary>
    public IList<string>? To { get; set; }

    /// <summary>
    /// Actual message content to be sent.
    /// Required if <see cref="Template"/> is not specified.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// The template to use.
    /// Required if <see cref="Body"/> is not specified.
    /// </summary>
    public MessageSourceTemplate? Template { get; set; }
}
