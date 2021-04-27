using Falu.Core;
using Falu.Payments.Mpesa;
using System;

namespace Falu.Payments
{
    /// <summary>
    /// Represents a transaction made by the business to customer or another business.
    /// </summary>
    public class Transfer : TransferPatchModel, IHasId, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Amount of the transfer in smallest currency unit.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Status of the transfer
        /// </summary>
        public TransferStatus Status { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Time at which the transfer succeeded. Only populated when successful.
        /// </summary>
        public DateTimeOffset? Succeeded { get; set; }

        /// <summary>
        /// The type of the Transfer.
        /// An additional property is populated on the Transfer with a name matching this value.
        /// It contains additional information specific to the Transfer type.
        /// </summary>
        public TransferType Type { get; set; }

        /// <summary>
        /// Purpose of the transfer.
        /// </summary>
        public TransferPurpose Purpose { get; set; }

        /// <summary>
        /// If this is an MPESA transfer, this contains details about the MPESA transfer.
        /// </summary>
        public TransferMpesaDetails Mpesa { get; set; }

        /// <summary>
        /// Details about failure if the transfer is in failed state.
        /// </summary>
        public FailureDetails Failure { get; set; }

        /// <summary>
        /// Identifier of the reversal, if transfer has been reversed.
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
