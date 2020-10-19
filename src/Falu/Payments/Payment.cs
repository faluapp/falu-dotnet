using Falu.Core;
using System;

namespace Falu.Payments
{
    /// <summary>
    /// Represents a Payment incoming, outgoing or within.
    /// </summary>
    public class Payment : PaymentPatchModel, IHasId, IHasCreated, IHasUpdated, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// Amount of the payment in smallest currency unit.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Status of the payment
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// The type of the Payment.
        /// An additional property is populated on the Payment with a name matching this value.
        /// It contains additional information specific to the Payment type.
        /// </summary>
        public PaymentType Type { get; set; }

        /// <summary>
        /// If this is an MPESA Payment, this contains details about the MPESA payment.
        /// </summary>
        public PaymentMpesaDetails Mpesa { get; set; }

        /// <summary>
        /// Identifier of the reversal, if payment has been reversed.
        /// </summary>
        public string ReversalId { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }
    }
}
