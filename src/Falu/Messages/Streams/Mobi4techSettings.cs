namespace Falu.Messages.Streams
{
    /// <summary>
    /// Settings for using the Mobi4tech provider.
    /// </summary>
    public class Mobi4techSettings
    {
        /// <summary>
        /// Application username for authentication.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Application password for authentication.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Authentication Key to access the API.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Identifier of the Plan to be used.
        /// </summary>
        public string PlanId { get; set; }

        /// <summary>
        /// Identifier of the sender to be used.
        /// This can be the shortcode.
        /// </summary>
        public string SenderId { get; set; }
    }
}