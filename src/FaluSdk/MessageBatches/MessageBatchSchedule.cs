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
    /// ISO8601 duration for which the message batch is/was to be delayed before sending.
    /// </summary>
    public string? Delay { get; set; }
}
