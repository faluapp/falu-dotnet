using Falu.Core;

namespace Falu.Transfers;

/// <summary>
/// A model representing details that can be changed about a transfer.
/// </summary>
public class TransferUpdateOptions : IHasDescription, IHasMetadata
{
    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
