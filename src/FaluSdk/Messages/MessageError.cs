namespace Falu.Messages;

///
public class MessageError
{
    /// <summary>
    /// A short machine-readable string giving the reason for message failure.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// A human-readable message that explains the reason for message failure.
    /// These message can be shown to your user.
    /// </summary>
    public string? Message { get; set; }
}
