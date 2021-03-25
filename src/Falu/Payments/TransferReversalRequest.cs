using Falu.Payments.Reversals;

namespace Falu.Payments
{
    /// <summary>
    /// Information for initiating a reversal for a transfer.
    /// </summary>
    public class TransferReversalRequest : ReversalPatchModel
    {
        /// <summary>
        /// Identifier of the Transfer to reverse.
        /// </summary>
        public string TransferId { get; set; }

        /// <summary>
        /// Reason for the reversal.
        /// </summary>
        public ReversalReason Reason { get; set; }
    }
}
