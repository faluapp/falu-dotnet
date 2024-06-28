using Falu.Core;

namespace Falu.Transfers;

/// <summary>
/// Information for creating a transfer.
/// </summary>
public class TransferCreateOptions : IHasCurrency, IHasDescription, IHasMetadata
{
    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <summary>
    /// Amount of the payment in smallest currency unit.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Purpose of the transfer.
    /// </summary>
    public string? Purpose { get; set; }

    /// <summary>
    /// Details about initiation of an MPESA transfer to a customer or another business.
    /// </summary>
    public TransferCreateOptionsMpesa? Mpesa { get; set; }

    /// <summary>
    /// Identifier of the Customer this Transfer belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
