using Falu.Core;

namespace Falu.MessageBatches;

/// <summary>
/// A model representing details that can be changed about a message batch.
/// </summary>
public class MessageBatchPatchModel : IHasMetadata
{
    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
