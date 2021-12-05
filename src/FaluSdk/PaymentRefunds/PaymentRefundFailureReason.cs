using System.Runtime.Serialization;

namespace Falu.PaymentRefunds;

/// <summary>
/// Reason for failure of a payment refund.
/// </summary>
public enum PaymentRefundFailureReason
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
