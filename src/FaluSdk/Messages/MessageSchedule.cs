namespace Falu.Messages;

/// <summary>
/// Information about scheduling for a message.
/// </summary>
public class MessageSchedule
{
    /// <summary>
    /// The <see href="https://en.wikipedia.org/wiki/ISO_8601#Durations">ISO 8601 duration</see>
    /// for which the message is/was to be delayed before sending.
    /// </summary>
    public DateTimeOffset Time { get; set; }

    /// <summary>
    /// ISO8601 duration for which the message is/was be delayed before sending.
    /// </summary>
    public string? Delay { get; set; }
}
