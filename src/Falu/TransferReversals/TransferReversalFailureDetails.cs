using System;

namespace Falu.TransferReversals
{
    /// <summary>
    /// Details about failure of a transfer reversal.
    /// </summary>
    public class TransferReversalFailureDetails
    {
        /// <summary>
        /// Reason for failure.
        /// </summary>
        public TransferReversalFailureReason Reason { get; set; }

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
