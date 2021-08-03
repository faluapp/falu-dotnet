namespace Falu.MessageStreams
{
    /// <summary>
    /// Represents the kind of provider used to send messages in a message stream.
    /// </summary>
    public enum MessageStreamProviderType
    {
        /// <summary>
        /// Backed by <see href="http://www.crossgatesolutions.com/"/>
        /// </summary>
        Crossgate,

        /// <summary>
        /// Backed by <see href="https://mobi4tech.co.ke/"/>
        /// </summary>
        Mobi4tech,

        /// <summary>
        /// Backed by <see href="http://mtechcomm.com/"/>
        /// </summary>
        Mtech,
    }
}
