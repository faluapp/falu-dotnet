namespace Falu.PaymentRefunds
{
    /// <summary>
    /// Details about failure of a payment refund.
    /// </summary>
    public class PaymentRefundFailureDetails
    {
        /// <summary>
        /// Reason for failure.
        /// </summary>
        public PaymentRefundFailureReason Reason { get; set; }

        /// <summary>
        /// Time at which failure occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Failure message as received from the provider.
        /// </summary>
        public string? Detail { get; set; }
    }
}
