using System.Runtime.Serialization;

namespace Falu.PaymentReversals
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
        Timeout,

        ///
        Other,
    }
}
