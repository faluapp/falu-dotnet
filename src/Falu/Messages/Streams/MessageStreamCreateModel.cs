namespace Falu.Messages.Streams
{
    /// <summary>
    /// Model for creating a message stream.
    /// </summary>
    public class MessageStreamCreateModel : MessagePatchModel
    {
        /// <summary>
        /// A string of 3-50 characters used for easier identification of a message stream.
        /// This value cannot be changed after creation.
        /// Allowed characters are numbers, lowercase ASCII letters, and ‘-’, ‘_’ characters,
        /// and the string has to start with a letter.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The type of stream to be created.
        /// </summary>
        public MessageStreamType Type { get; set; } = MessageStreamType.Transactional;
    }
}
