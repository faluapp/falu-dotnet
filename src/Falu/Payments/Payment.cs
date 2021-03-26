using Falu.Core;
using Falu.Payments.Mpesa;
using System;

namespace Falu.Payments
{
    /// <summary>
    /// Represents a transaction done by a customer to the business.
    /// </summary>
    public class Payment : PaymentPatchModel, IHasId, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Amount of the payment in smallest currency unit.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Status of the payment
        /// </summary>
        public PaymentStatus Status { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Time at which the payment succeeded. Only populated when successful.
        /// </summary>
        public DateTimeOffset? Succeeded { get; set; }

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
        /// Details about failure if the payment is in failed state.
        /// </summary>
        public FailureDetails Failure { get; set; }

        /// <summary>
        /// Identifier of the reversal, if payment has been reversed.
        /// </summary>
        public string ReversalId { get; set; }

        /// <inheritdoc/>
        public string WorkspaceId { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }
    }
}
