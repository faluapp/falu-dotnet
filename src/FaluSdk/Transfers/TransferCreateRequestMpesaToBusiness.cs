namespace Falu.Transfers
{
    /// <summary>
    /// Information for initiating an outgoing payment to business via MPESA.
    /// </summary>
    public class TransferCreateRequestMpesaToBusiness
    {
        /// <summary>
        /// The business short code to be debited.
        /// The movement of funds between the respective short codes must be allowed.
        /// </summary>
        public string? Source { get; set; }

        /// <summary>
        /// The business short code to be credited.
        /// The movement of funds between the respective short codes must be allowed.
        /// </summary>
        public string? Destination { get; set; }

        /// <summary>
        /// Indicates if the transfer is from MMF to Utility.
        /// </summary>
        public bool MMF { get; set; }
    }
}
