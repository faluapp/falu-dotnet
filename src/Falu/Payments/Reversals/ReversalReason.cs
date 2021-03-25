namespace Falu.Payments.Reversals
{
    /// <summary>
    /// The reason for reversing a payment or transfer.
    /// </summary>
    public enum ReversalReason
    {
        ///
        Duplicate,

        ///
        Fraudulent,

        ///
        CustomerRequested,

        ///
        Other,
    }
}
