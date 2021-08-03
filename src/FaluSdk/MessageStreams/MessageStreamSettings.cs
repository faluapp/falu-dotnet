namespace Falu.MessageStreams
{
    /// <summary>
    /// Settings of a message stream.
    /// </summary>
    public class MessageStreamSettings
    {
        /// <summary>
        /// Configuration settings for working with Mtech
        /// </summary>
        public MtechSettings? Mtech { get; set; }

        /// <summary>
        /// Configuration settings for working with Mobi4tech.
        /// </summary>
        public Mobi4techSettings? Mobi4tech { get; set; }

        /// <summary>
        /// Configuration settings for working with Crossgate
        /// </summary>
        public CrossgateSettings? Crossgate { get; set; }
    }
}
