namespace Falu.MessageSuppressions;

/// <summary>
/// Information for creating a message suppression
/// </summary>
public class MessageSuppressionCreateRequest 
{
    /// <summary>
    /// Destination phone number in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// </summary>
    public string? To { get; set; }

    /// <summary>
    /// The stream to use.
    /// It can either be the name or unique identifier of the stream.
    /// If not provided, message will default to the "transactional" stream.
    /// </summary>
    public string Stream { get; set; } = "transactional";
}
