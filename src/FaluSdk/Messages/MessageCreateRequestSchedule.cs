namespace Falu.Messages;

/// <summary>
/// Information about scheduling of a message.
/// </summary>
public class MessageCreateRequestSchedule
{
    /// <summary>
    /// The <see href="https://en.wikipedia.org/wiki/ISO_8601#Durations">ISO 8601 duration</see>
    /// for which the message(s) should be delayed before sending, relative to the current server time.
    /// Between 5 minutes and 30 days in the future.
    /// Required if <see cref="Delay"/> is not specified.
    /// </summary>
    public DateTimeOffset? Time { get; set; }

    /// <summary>
    /// ISO8601 duration for which the message should be delayed
    /// before sending, relative to the current server time.
    /// Between 5 minutes and 30 days.
    /// Required if <see cref="Time"/> is not specified.
    /// </summary>
    public string? Delay { get; set; }

    /// <summary>
    /// Convert a <see cref="DateTimeOffset"/> to a <see cref="MessageCreateRequestSchedule"/>.
    /// </summary>
    /// <param name="time">Value for <see cref="Time"/></param>
    public static implicit operator MessageCreateRequestSchedule(DateTimeOffset time) => new() { Time = time, };

    /// <summary>
    /// Convert a <see cref="string"/> to a <see cref="MessageCreateRequestSchedule"/>.
    /// </summary>
    /// <param name="delay">Value for <see cref="Delay"/></param>
    public static implicit operator MessageCreateRequestSchedule(string delay) => new() { Delay = delay, };
}
