using Falu.Core;
using System;

namespace Falu.Payments.Reversals
{
    /// <summary>
    /// Represents a reversal of a Payment.
    /// </summary>
    public abstract class AbstractReversal : ReversalPatchModel, IHasId, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase. This is pulled from the Payment.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Amount reversed in smallest currency unit.
        /// This is pulled from the Payment.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Reason for the reversal.
        /// </summary>
        public ReversalReason Reason { get; set; }

        /// <summary>
        /// Status of the reversal. 
        /// </summary>
        public ReversalStatus Status { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Details of the reversal if done via MPESA.
        /// Only populated if the payment being reversed use an MPESA intrument.
        /// </summary>
        public ReversalMpesaDetails Mpesa { get; set; }

        /// <summary>
        /// Details about failure if the reversal is in failed state.
        /// </summary>
        public FailureDetails Failure { get; set; }

        /// <inheritdoc/>
        public string WorkspaceId { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }
    }
}
