namespace Falu.Core;

/// <summary>
/// Interface that identifies objects with a Metadata property of type <see cref="Dictionary{TKey, TValue}"/>.
/// </summary>
public interface IHasMetadata
{
    /// <summary>
    /// Set of key-value pairs that you can attach to an object. This can be useful
    /// for storing additional information about the object in a structured format.
    /// </summary>
    public Dictionary<string, string>? Metadata { get; set; }
}
