namespace Falu.Core;

/// <summary>
/// Represents redaction information of an object.
/// </summary>
public class DataRedaction
{
    /// <summary>
    /// Indicates whether the object and its related objects have been redacted or not.
    /// </summary>
    public DataRedactionStatus Status { get; set; }
}

/// <summary>
/// Represents the status of a redaction.
/// </summary>
public enum DataRedactionStatus
{
    /// <summary>
    /// The object and its related objects have been redacted.
    /// </summary>
    Redacted,

    /// <summary>
    /// The object has been redacted, and its related objects are in the process of being redacted.
    /// This process may take up to a week.
    /// </summary>
    Processing,
}
