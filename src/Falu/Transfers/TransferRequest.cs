using Falu.Core;

namespace Falu.Transfers
{
    /// <summary>
    /// Information for initiating a transfer.
    /// </summary>
    public class TransferRequest : TransferPatchModel, IHasCurrency
    {
        /// <inheritdoc/>
        public string? Currency { get; set; }

        /// <summary>
        /// Amount of the payment in smallest currency unit.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Purpose of the transfer.
        /// </summary>
        public TransferPurpose? Purpose { get; set; }

        /// <summary>
        /// Details about initiation of an MPESA transfer to a customer or another business.
        /// </summary>
        public TransferRequestMpesa? Mpesa { get; set; }
    }
}
