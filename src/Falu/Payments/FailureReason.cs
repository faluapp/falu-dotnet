namespace Falu.Payments
{
    /// <summary>
    /// Reason for failure of a payment, transfer or revesal.
    /// </summary>
    public enum FailureReason
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
