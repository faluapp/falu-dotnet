namespace Falu.Payments.Reversals
{
    /// <summary>
    /// Represents a reversal of a Transfer.
    /// </summary>
    public class TransferReversal : AbstractReversal
    {
        /// <summary>
        /// Identifier of the Transfer reversed.
        /// </summary>
        public string TransferId { get; set; }
    }
}
