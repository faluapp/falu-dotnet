namespace Falu.Transfers
{
    /// <summary>
    /// Information for initiating a transfer to customer or another business via MPESA.
    /// </summary>
    public class TransferRequestMpesa
    {
        /// <summary>
        /// Details about initiation of an MPESA payment to customer.
        /// This is also referred to as a Business To Customer (B2C) transfer.
        /// </summary>
        public TransferRequestMpesaToCustomer Customer { get; set; }

        /// <summary>
        /// Details about initiation of an MPESA payment to another business.
        /// This is also referred to as a Business To Business (B2B) transfer.
        /// </summary>
        public TransferRequestMpesaToBusiness Business { get; set; }
    }
}
