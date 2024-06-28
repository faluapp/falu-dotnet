using Falu.Core;

namespace Falu.TransferReversals;

/// <summary>
/// Information for creating a transfer reversal.
/// </summary>
public class TransferReversalCreateOptions : IHasDescription, IHasMetadata
{
    /// <summary>
    /// Identifier of the Transfer to reverse.
    /// </summary>
    public string? Transfer { get; set; }

    /// <summary>
    /// Reason for the reversal.
    /// </summary>
    public string? Reason { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

}
