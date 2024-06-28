using Falu.Core;

namespace Falu.PaymentRefunds;

/// <summary>
/// Information for creating a payment refund.
/// </summary>
public class PaymentRefundCreateOptions : IHasDescription, IHasMetadata
{
    /// <summary>
    /// Identifier of the Payment to reverse.
    /// </summary>
    public string? Payment { get; set; }

    /// <summary>
    /// Reason for the reversal.
    /// </summary>
    public string? Reason { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
