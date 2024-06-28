using Falu.Core;

namespace Falu.Payments;

/// <summary>
/// Information for creating a payment.
/// </summary>
public class PaymentCreateOptions : IHasCurrency, IHasDescription, IHasMetadata
{
    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <summary>
    /// Amount of the payment in smallest currency unit.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Details about initiation by MPESA's STK Push
    /// </summary>
    public PaymentCreateOptionsMpesa? Mpesa { get; set; }

    /// <summary>
    /// Identifier of the Customer this Payment belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
