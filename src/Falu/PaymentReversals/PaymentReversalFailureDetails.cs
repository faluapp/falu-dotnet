using System;

namespace Falu.PaymentReversals
{
    /// <summary>
    /// Details about failure of a payment reversal.
    /// </summary>
    public class PaymentReversalFailureDetails
    {
        /// <summary>
        /// Reason for failure.
        /// </summary>
        public PaymentReversalFailureReason Reason { get; set; }

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
