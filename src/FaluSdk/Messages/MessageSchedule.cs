namespace Falu.Messages;

/// <summary>
/// Information about scheduling for a message.
/// </summary>
public class MessageSchedule
{
    /// <summary>
    /// Time at which the message is/was scheduled to be sent.
    /// </summary>
    public DateTimeOffset Time { get; set; }

    /// <summary>
    /// ISO8601 duration for which the message is/was be delayed before sending.
    /// </summary>
    public string? Delay { get; set; }
}
