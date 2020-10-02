namespace Falu.Messages
{
    /// <summary>
    /// Information for creating and sending message.
    /// </summary>
    public class MessageCreateRequest : MessagePatchModel
    {
        /// <summary>
        /// Destination phone number in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
        /// </summary>
        /// <example>+254722000000</example>
        public string To { get; set; }

        /// <summary>
        /// Provider for the sending service.
        /// Only required when the message must be sent by a specific provider.
        /// </summary>
        public MessageProviderType? Provider { get; set; }

        /// <summary>
        /// Actual message content to be sent.
        /// Required if <see cref="Template"/> is not specified.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// The template to use.
        /// Required if <see cref="Body"/> is not specified.
        /// </summary>
        public MessageTemplate Template { get; set; }
    }
}
