namespace Falu.Core;

/// <summary>
/// Interface that identifies objects with an <c>Redaction</c> property.
/// </summary>
public interface IHasRedaction
{
    /// <summary>
    /// Redaction information of this object.
    /// If the object is not redacted, this field will be null.
    /// </summary>
    DataRedaction? Redaction { get; set; }
}
