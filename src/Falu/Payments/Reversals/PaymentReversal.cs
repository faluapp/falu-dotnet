namespace Falu.Payments.Reversals
{
    /// <summary>
    /// Represents a reversal of a Payment.
    /// </summary>
    public class PaymentReversal : AbstractReversal
    {
        /// <summary>
        /// Identifier of the Payment reversed.
        /// </summary>
        public string PaymentId { get; set; }
    }
}
