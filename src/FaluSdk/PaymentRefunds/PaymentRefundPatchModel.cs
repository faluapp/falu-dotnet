using Falu.Core;

namespace Falu.PaymentRefunds;

/// <summary>
/// A model representing details that can be changed about a payment refund.
/// </summary>
public class PaymentRefundPatchModel : IHasDescription, IHasMetadata
{
    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
