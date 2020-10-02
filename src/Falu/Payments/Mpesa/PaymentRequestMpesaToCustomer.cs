namespace Falu.Payments.Mpesa
{
    /// <summary>
    /// Information for initiating an outgoing payment to customer via MPESA.
    /// </summary>
    public class PaymentRequestMpesaToCustomer
    {
        /// <summary>
        /// The phone number to which the money is to be sent. This number must be in MSISDN format
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The kind of command being made. It can only be one of the following:
        /// SalaryPayment, BusinessPayment, or PromotionPayment
        /// </summary>
        public MpesaCommandKind? Kind { get; set; }
    }
}
