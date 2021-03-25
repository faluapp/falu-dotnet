namespace Falu.Payments.Mpesa
{
    /// <summary>
    /// Represents the provider details for a MPESA transfer.
    /// </summary>
    public class TransferMpesaDetails : BaseMpesaDetails
    {
        /// <summary>
        /// Type of command.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Destination of where the transfer is/was sent to.
        /// </summary>
        public string Destination { get; set; }

        /// <summary>
        /// Charges for the transaction in the smallest currency unit.
        /// </summary>
        public long? Charges { get; set; }

        /// <summary>
        /// Details of the receiver.
        /// </summary>
        public string Receiver { get; set; }
    }
}
