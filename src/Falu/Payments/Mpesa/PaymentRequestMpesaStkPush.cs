namespace Falu.Payments.Mpesa
{
    /// <summary>
    /// Information for initiating an incoming payment from customer to business via MPESA.
    /// </summary>
    public class PaymentRequestMpesaStkPush
    {
        /// <summary>
        /// The phone number representing the account to be charged.
        /// This should be in MSISDN format
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The reference that the payment will be made in.
        /// This can be an account number.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// The kind of STK Push to initiate.
        /// </summary>
        public MpesaStkPushTransactionType? Kind { get; set; }

        /// <summary>
        /// The shortcode of the receiver.
        /// When not provided, it defaults to the default recepient.
        /// This value is usually required and different from the business short code when using TillNumbers.
        /// </summary>
        public string Destination { get; set; }
    }
}
