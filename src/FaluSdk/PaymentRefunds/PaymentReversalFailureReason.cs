using System.Runtime.Serialization;

namespace Falu.PaymentRefunds
{
    /// <summary>
    /// Reason for failure of a payment reversal.
    /// </summary>
    public enum PaymentReversalFailureReason
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
}
