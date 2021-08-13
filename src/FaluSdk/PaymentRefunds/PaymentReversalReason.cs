namespace Falu.PaymentRefunds
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
