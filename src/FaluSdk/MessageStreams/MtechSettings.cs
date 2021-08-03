namespace Falu.MessageStreams
{
    /// <summary>
    /// Settings for using the Mtech provider.
    /// </summary>
    public class MtechSettings
    {
        /// <summary>
        /// Application user-name for authentication.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Application password for authentication.
        /// </summary>
        public string? Password { get; set; }

        /// <summary>
        /// Short code for sending messages.
        /// </summary>
        public string? ShortCode { get; set; }
    }
}
