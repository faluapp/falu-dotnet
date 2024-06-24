using Falu.Core;

namespace Falu.MessageStreams;

/// <summary>
/// Represents the details about a message stream that can be patched.
/// </summary>
public class MessageStreamUpdateOptions : IHasDescription, IHasMetadata
{
    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
