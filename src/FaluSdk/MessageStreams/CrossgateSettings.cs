namespace Falu.MessageStreams
{
    /// <summary>
    /// Settings for using the Crossgate provider.
    /// </summary>
    public class CrossgateSettings
    {
        /// <summary>
        /// The application identifier for making requests with.
        /// </summary>
        public string? AppKey { get; set; }

        /// <summary>
        /// The application secret for making requests with.
        /// </summary>
        public string? AppSecret { get; set; }

        /// <summary>
        /// The profile to use when making requests.
        /// </summary>
        public string? ProfileId { get; set; }
    }
}
