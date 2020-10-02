namespace Falu.Payments
{
    /// <summary>
    /// The status of a payment.
    /// </summary>
    public enum PaymentStatus
    {
        ///
        Pending,

        ///
        InTransit,

        ///
        Succeeded,

        ///
        Cancelled,

        ///
        Failed,
    }
}
