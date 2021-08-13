namespace Falu.PaymentRefunds
{
    /// <summary>
    /// The reason for reversing a payment.
    /// </summary>
    public enum PaymentRefundReason
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
