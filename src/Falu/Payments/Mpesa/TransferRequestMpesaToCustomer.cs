namespace Falu.Payments.Mpesa
{
    /// <summary>
    /// Information for initiating an outgoing payment to customer via MPESA.
    /// </summary>
    public class TransferRequestMpesaToCustomer
    {
        /// <summary>
        /// The business short code to be debited.
        /// This code must be configured in the workspace.
        /// When not provided, either the default outgoing business code
        /// or the first business code for the workspace is used.
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// The phone number to which the money is to be sent.
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// The kind of command being made. It can only be one of the following:
        /// SalaryPayment, BusinessPayment, or PromotionPayment
        /// </summary>
        public MpesaCommandKind? Kind { get; set; }
    }
}
