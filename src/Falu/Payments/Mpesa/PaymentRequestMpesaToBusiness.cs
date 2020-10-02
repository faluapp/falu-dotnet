namespace Falu.Payments.Mpesa
{
    /// <summary>
    /// Information for initiating an outgoing payment to business via MPESA.
    /// </summary>
    public class PaymentRequestMpesaToBusiness
    {
        /// <summary>
        /// The business short code to be debited.
        /// The movement of funds between the respective short codes must be allowed.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The business short code to be credited.
        /// The movement of funds between the respective short codes must be allowed.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// The kind of command being made. It can only be one of the following:
        /// BusinessPayBill, BusinessBuyGoods, DisburseFundsToBusiness, BusinessToBusinessTransfer,
        /// or BusinessTransferFromMMFToUtility
        /// </summary>
        public MpesaCommandKind? Kind { get; set; }
    }
}
