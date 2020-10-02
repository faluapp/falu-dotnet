using Falu.Payments.Mpesa;

namespace Falu.Payments
{
    /// <summary>
    /// Information for initiating a payment.
    /// </summary>
    public class PaymentRequest
    {
        /// <summary>
        /// Amount of the payment.
        /// </summary>
        public int Amount { get; set; }

        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Details about initiation by MPESA's STK Push
        /// </summary>
        public PaymentRequestMpesaStkPush StkPush { get; set; }

        /// <summary>
        /// Details about initiation of an MPESA payment to customer.
        /// This is also referred to as a Business To Customer (B2C) transfer.
        /// </summary>
        public PaymentRequestMpesaToCustomer MpesaToCustomer { get; set; }

        /// <summary>
        /// Details about initiation of an MPESA payment to another business.
        /// This is also referred to as a Business To Business (B2B) transfer.
        /// </summary>
        public PaymentRequestMpesaToBusiness MpesaToBusiness { get; set; }
    }
}
