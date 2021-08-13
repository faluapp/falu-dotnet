namespace Falu.PaymentRefunds
{
    /// <summary>
    /// Information for initiating a payment reversal.
    /// </summary>
    public class PaymentRefundRequest : PaymentRefundPatchModel
    {
        /// <summary>
        /// Identifier of the Payment to reverse.
        /// </summary>
        public string? PaymentId { get; set; }

        /// <summary>
        /// Reason for the reversal.
        /// </summary>
        public PaymentRefundReason? Reason { get; set; }
    }
}
