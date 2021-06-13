using System;

namespace Falu.Payments
{
    /// <summary>
    /// Details about failure of a payment, transfer or reversal.
    /// </summary>
    public class FailureDetails
    {
        /// <summary>
        /// Reason for failure.
        /// </summary>
        public FailureReason Reason { get; set; }

        /// <summary>
        /// Time at which failure occurred.
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Failure message as recevied from teh provider.
        /// </summary>
        public string Detail { get; set; }
    }
}
