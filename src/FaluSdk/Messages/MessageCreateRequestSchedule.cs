namespace Falu.Messages;

/// <summary>
/// Information about scheduling of a message.
/// </summary>
public class MessageCreateRequestSchedule
{
    /// <summary>
    /// Time at which the message should be sent.
    /// Between 5 minutes and 30 days in the future.
    /// Required if <see cref="Delay"/> is not specified.
    /// </summary>
    public DateTimeOffset? Time { get; set; }

    /// <summary>
    /// Duration for which the message was be delayed before sending,
    /// relative to the current server time.
    /// Between 5 minutes and 30 days.
    /// Required if <see cref="Time"/> is not specified.
    /// </summary>
    /// <example>1.06:00:00</example>
    public TimeSpan? Delay { get; set; }

    /// <summary>
    /// Convert a <see cref="DateTimeOffset"/> to a <see cref="MessageCreateRequestSchedule"/>.
    /// </summary>
    /// <param name="time">Value for <see cref="Time"/></param>
    public static implicit operator MessageCreateRequestSchedule(DateTimeOffset time) => new() { Time = time, };

    /// <summary>
    /// Convert a <see cref="TimeSpan"/> to a <see cref="MessageCreateRequestSchedule"/>.
    /// </summary>
    /// <param name="delay">Value for <see cref="Delay"/></param>
    public static implicit operator MessageCreateRequestSchedule(TimeSpan delay) => new() { Delay = delay, };
}
