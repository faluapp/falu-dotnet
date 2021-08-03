namespace Falu.PaymentReversals
{
    /// <summary>
    /// The reason for reversing a payment.
    /// </summary>
    public enum PaymentReversalReason
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
