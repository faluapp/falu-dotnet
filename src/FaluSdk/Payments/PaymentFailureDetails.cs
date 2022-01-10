namespace Falu.Payments;

/// <summary>
/// Details about failure of a payment.
/// </summary>
public class PaymentFailureDetails
{
    /// <summary>
    /// Reason for failure.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Time at which failure occurred.
    /// </summary>
    public DateTimeOffset Timestamp { get; set; }

    /// <summary>
    /// Failure message as received from the provider.
    /// </summary>
    public string? Detail { get; set; }
}
