namespace Falu.Messages.Streams
{
    /// <summary>
    /// Settings of a message stream.
    /// </summary>
    public class MessageStreamSettings
    {
        /// <summary>
        /// Configuration settings for working with Mtech
        /// </summary>
        public MtechSettings Mtech { get; set; }

        /// <summary>
        /// Configuration settings for working with Crossgate
        /// </summary>
        public CrossgateSettings Crossgate { get; set; }
    }
}
