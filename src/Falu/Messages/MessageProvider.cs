namespace Falu.Messages
{
    /// <summary>
    /// Details about the provider used to send a message.
    /// </summary>
    public class MessageProvider
    {
        /// <summary>
        /// Provider for the sending service.
        /// </summary>
        public MessageProviderType? Type { get; set; }

        /// <summary>
        /// Unique identifier for the request as provided by the provider.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// The error message from the provider.
        /// </summary>
        public string Error { get; set; }
    }
}
