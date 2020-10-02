namespace Falu.Payments.Reversals
{
    /// <summary>
    /// The reason why a payment has been reversed.
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
