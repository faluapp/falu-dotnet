using Falu.Core;

namespace Falu.Payments;

/// <summary>
/// Information for creating a payment.
/// </summary>
public class PaymentCreateOptions : PaymentUpdateOptions, IHasCurrency
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
}
