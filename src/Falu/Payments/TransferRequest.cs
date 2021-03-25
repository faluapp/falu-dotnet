using Falu.Payments.Mpesa;

namespace Falu.Payments
{
    /// <summary>
    /// Information for initiating a transfer.
    /// </summary>
    public class TransferRequest : TransferPatchModel
    {
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
        /// Details about initiation of an MPESA transfer to a customer or another business.
        /// </summary>
        public TransferRequestMpesa Mpesa { get; set; }
    }
}
