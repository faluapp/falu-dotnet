namespace Falu.Messages.Streams
{
    /// <summary>
    /// Model for creating a message stream.
    /// </summary>
    public class MessageStreamCreateModel : MessageStreamPatchModel
    {
        /// <summary>
        /// A string of 3-50 characters used for easier identification of a message stream.
        /// This value cannot be changed after creation.
        /// Allowed characters are numbers, lowercase ASCII letters, and ‘-’, ‘_’ characters,
        /// and the string has to start with a letter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Type of stream.
        /// </summary>
        public MessageStreamType Type { get; set; } = MessageStreamType.Transactional;

        /// <summary>
        /// Provider to be used.
        /// </summary>
        public MessageProviderType? Provider { get; set; }
    }
}
