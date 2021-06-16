using System.Runtime.Serialization;

namespace Falu.Transfers
{
    /// <summary>
    /// Reason for failure of a transfer.
    /// </summary>
    public enum TransferFailureReason
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
