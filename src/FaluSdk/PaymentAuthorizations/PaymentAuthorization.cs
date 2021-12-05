using Falu.Core;
using Falu.Payments;

namespace Falu.PaymentAuthorizations
{
    /// <summary>
    /// Represents a payment authorization.
    /// </summary>
    public class PaymentAuthorization : PaymentAuthorizationPatchModel, IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string? Id { get; set; }

        /// <inheritdoc/>
        public string? Currency { get; set; }

        /// <summary>
        /// Amount that was authorized or rejected, in smallest currency unit.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Whether the authorization has been approved.
        /// </summary>
        public bool Approved { get; set; }

        /// <summary>
        /// Status of the payment authorization.
        /// </summary>
        public PaymentAuthorizationStatus Status { get; set; }

        /// <summary>
        /// Reason for the given status of the payment authorization.
        /// </summary>
        public PaymentAuthorizationStatusReason? Reason { get; set; }

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
        public PaymentMpesaDetails? Mpesa { get; set; }

        /// <summary>
        /// Identifier of the payment created after the authorization is approved and closed.
        /// </summary>
        public string? PaymentId { get; set; }

        /// <inheritdoc/>
        public string? WorkspaceId { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string? Etag { get; set; }
    }
}
