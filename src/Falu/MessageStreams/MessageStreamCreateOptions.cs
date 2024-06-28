using Falu.Core;

namespace Falu.MessageStreams;

/// <summary>
/// Information for creating a message stream.
/// </summary>
public class MessageStreamCreateOptions : IHasDescription, IHasMetadata
{
    /// <summary>
    /// A string used for easier identification.
    /// This value cannot be changed after creation.
    /// Allowed characters are numbers, lowercase ASCII letters, and ‘-’, ‘_’ characters,
    /// and the string has to start with a letter.
    /// </summary>
    public string? Name { get; set; }

    /// <summary>
    /// Type of stream.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Provider to be used.
    /// </summary>
    public string? Provider { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
