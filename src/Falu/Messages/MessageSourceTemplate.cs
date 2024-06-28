namespace Falu.Messages;

/// <summary>
/// Information about the template used to send a message.
/// </summary>
public class MessageSourceTemplate
{
    /// <summary>
    /// Unique identifier of the template used.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Model applied when rending the template.
    /// </summary>
    public object? Model { get; set; }
}
