namespace Falu.Core;

/// <summary>
/// Represents an error in a Falu object.
/// </summary>
public class ObjectError
{
    /// <summary>
    /// A short machine-readable string giving the reason for the failure.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// Time at which failure occurred, if any.
    /// </summary>
    public DateTimeOffset? Timestamp { get; set; }

    /// <summary>
    /// A human-readable message giving the reason for the failure.
    /// This message can be shown to your user.
    /// </summary>
    public string? Description { get; set; }
}
