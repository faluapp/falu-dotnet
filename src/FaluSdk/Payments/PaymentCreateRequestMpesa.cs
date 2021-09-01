namespace Falu.Payments
{
    /// <summary>
    /// Information for initiating a payment from a customer to the business via MPESA.
    /// </summary>
    public class PaymentCreateRequestMpesa
    {
        /// <summary>
        /// The phone number representing the account to be charged.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// The reference that the payment will be made in.
        /// This can be an account number.
        /// </summary>
        public string? Reference { get; set; }

        /// <summary>
        /// Set true if the payment to be initiated is to be made to a paybill;
        /// false, the payment is made to a BuyGoods till.
        /// </summary>
        public bool Paybill { get; set; }

        /// <summary>
        /// The shortcode of the receiver.
        /// When not provided, it defaults to the default recipient.
        /// When not provided, either the default incoming business code
        /// or the first business code for the workspace is used depending on the <c>Kind</c>.
        /// <br/>
        /// This value is usually required and different from the business short code when using BuyGoods.
        /// </summary>
        public string? Destination { get; set; }
    }
}
