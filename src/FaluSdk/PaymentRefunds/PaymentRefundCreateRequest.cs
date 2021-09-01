namespace Falu.PaymentRefunds
{
    /// <summary>
    /// Information for creating a payment refund.
    /// </summary>
    public class PaymentRefundCreateRequest : PaymentRefundPatchModel
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
