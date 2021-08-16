﻿namespace Falu.TransferReversals
{
    /// <summary>
    /// Information for initiating a reversal for a transfer.
    /// </summary>
    public class TransferReversalRequest : TransferReversalPatchModel
    {
        /// <summary>
        /// Identifier of the Transfer to reverse.
        /// </summary>
        public string? TransferId { get; set; }

        /// <summary>
        /// Reason for the reversal.
        /// </summary>
        public TransferReversalReason Reason { get; set; }
    }
}