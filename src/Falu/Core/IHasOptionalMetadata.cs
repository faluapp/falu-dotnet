namespace Falu.Core;

/// <summary>
/// Interface that identifies objects with a <c>Metadata</c> property of type <see cref="Dictionary{TKey, TValue}"/>.
/// </summary>
public interface IHasOptionalMetadata
{
    /// <summary>
    /// Set of key-value pairs that you can attach to an object. This can be useful
    /// for storing additional information about the object in a structured format.
    /// </summary>
    public Optional<Dictionary<string, string>?>? Metadata { get; set; }
}
