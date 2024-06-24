using Falu.Core;

namespace Falu.Messages;

/// <summary>
/// A model representing details that can be changed about a message.
/// </summary>
public class MessageUpdateOptions : IHasMetadata
{
    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
