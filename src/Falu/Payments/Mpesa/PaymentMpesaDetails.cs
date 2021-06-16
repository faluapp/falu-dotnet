namespace Falu.Payments.Mpesa
{
    /// <summary>
    /// Details about an MPESA Payment
    /// </summary>
    public class PaymentMpesaDetails : BaseMpesaDetails
    {
        /// <summary>
        /// Type of payment.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Reference the payment was made in.
        /// </summary>
        public string Reference { get; set; }

        /// <summary>
        /// Phone number that made the payment, in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Name of the entity making or that made the payment.
        /// </summary>
        public string Payer { get; set; }
    }
}
