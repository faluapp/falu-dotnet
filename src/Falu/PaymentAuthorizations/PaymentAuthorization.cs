using Falu.Core;
using Falu.Payments;
using System;

namespace Falu.PaymentAuthorizations
{
    /// <summary>
    /// Represents a payment authorization.
    /// </summary>
    public class PaymentAuthorization : PaymentAuthorizationPatchModel, IHasId, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// Amount that was authorized or rejected, in smallest currency unit.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Whether the authorization has been approved.
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Status of the payment authorization.
        /// </summary>
        public PaymentAuthorizationStatus Status { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Type of the payment requiring authorization.
        /// An additional property is populated on the authorization with a name matching this value.
        /// It contains additional information specific to the payment type.
        /// </summary>
        public PaymentType Type { get; set; }

        /// <summary>
        /// If this is an MPESA Payment, this contains details about the MPESA payment.
        /// </summary>
        public PaymentMpesaDetails Mpesa { get; set; }

        /// <inheritdoc/>
        public string WorkspaceId { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }
    }
}
