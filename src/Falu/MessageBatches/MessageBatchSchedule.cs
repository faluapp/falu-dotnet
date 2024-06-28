namespace Falu.MessageBatches;

/// <summary>
/// Information about scheduling for a message batch.
/// </summary>
public class MessageBatchSchedule
{
    /// <summary>
    /// Time at which the message batch is/was scheduled to be sent.
    /// </summary>
    public DateTimeOffset Time { get; set; }

    /// <summary>
    /// The <see href="https://en.wikipedia.org/wiki/ISO_8601#Durations">ISO 8601 duration</see>
    /// for which the message batch is/was to be delayed before sending.
    /// </summary>
    public string? Delay { get; set; }
}
