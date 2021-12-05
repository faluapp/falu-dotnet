using System.Runtime.Serialization;

namespace Falu.Payments;

/// <summary>
/// Reason for failure of a payment.
/// </summary>
public enum PaymentFailureReason
{
    ///
    Unknown,

    ///
    [EnumMember(Value = "insufficient_balance")]
    InsufficientBalance,

    ///
    [EnumMember(Value = "authentication_error")]
    AuthenticationError,

    ///
    [EnumMember(Value = "amount_out_of_bound")]
    AmountOutOfBound,

    ///
    Timeout,

    ///
    Other,
}
