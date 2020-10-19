using Falu.Core;
using System;

namespace Falu.Payments.Reversals
{
    /// <summary>
    /// Represents a reversal of a Payment.
    /// </summary>
    public class PaymentReversal : PaymentReversalPatchModel, IHasId, IHasCreated, IHasUpdated, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// Identifier of the Payment reversed.
        /// </summary>
        public string PaymentId { get; set; }

        /// <summary>
        /// Amount reversed in smallest currency unit.
        /// This is pulled from the Payment.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase. This is pulled from the Payment.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Reason for the reversal.
        /// </summary>
        public PaymentReversalReason Reason { get; set; }

        /// <summary>
        /// Status of the reversal. 
        /// </summary>
        public PaymentReversalStatus Status { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Details of the reversal if done via MPESA.
        /// Only populated if the payment being reversed use an MPESA intrument.
        /// </summary>
        public PaymentReversalMpesaDetails Mpesa { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }
    }
}
