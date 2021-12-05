namespace Falu.PaymentAuthorizations;

/// <summary>
/// The status of a payment authorization.
/// </summary>
public enum PaymentAuthorizationStatus
{
    /// <summary>
    /// The authorization was created and is awaiting approval or was approved and is awaiting capture.
    /// </summary>
    Pending,

    /// <summary>
    /// The authorization was declined or captured.
    /// </summary>
    Closed,

    /// <summary>
    /// The authorization was reversed by the payment provider or expired without capture.
    /// </summary>
    Reversed,
}
