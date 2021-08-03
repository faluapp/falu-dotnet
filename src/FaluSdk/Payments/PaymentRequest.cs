using Falu.Core;

namespace Falu.Payments
{
    /// <summary>
    /// Information for initiating a payment.
    /// </summary>
    public class PaymentRequest : PaymentPatchModel, IHasCurrency
    {
        /// <inheritdoc/>
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
