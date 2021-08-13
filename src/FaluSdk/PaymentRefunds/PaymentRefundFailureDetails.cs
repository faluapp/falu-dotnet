using System;

namespace Falu.PaymentRefunds
{
    /// <summary>
    /// Details about failure of a payment reversal.
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
        /// Failure message as recevied from teh provider.
        /// </summary>
        public string? Detail { get; set; }
    }
}
