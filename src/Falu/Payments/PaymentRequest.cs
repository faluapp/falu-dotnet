namespace Falu.Payments
{
    /// <summary>
    /// Information for initiating a payment.
    /// </summary>
    public class PaymentRequest : PaymentPatchModel
    {
        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase.
        /// </summary>
        public string? Currency { get; set; }

        /// <summary>
        /// Amount of the payment in smallest currency unit.
        /// </summary>
        public long Amount { get; set; }

        /// <summary>
        /// Details about initiation by MPESA's STK Push
        /// </summary>
        public PaymentRequestMpesa? Mpesa { get; set; }
    }
}
