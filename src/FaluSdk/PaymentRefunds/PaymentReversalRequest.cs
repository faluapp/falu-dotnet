namespace Falu.PaymentRefunds
{
    /// <summary>
    /// Information for initiating a payment reversal.
    /// </summary>
    public class PaymentReversalRequest : PaymentReversalPatchModel
    {
        /// <summary>
        /// Identifier of the Payment to reverse.
        /// </summary>
        public string? PaymentId { get; set; }

        /// <summary>
        /// Reason for the reversal.
        /// </summary>
        public PaymentReversalReason? Reason { get; set; }
    }
}
