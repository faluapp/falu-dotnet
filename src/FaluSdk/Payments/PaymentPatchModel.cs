using Falu.Core;

namespace Falu.Payments;

/// <summary>
/// A model representing details that can be changed about a payment.
/// </summary>
public class PaymentPatchModel : IHasDescription, IHasMetadata
{
    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
