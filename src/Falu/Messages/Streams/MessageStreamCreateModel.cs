namespace Falu.Messages.Streams
{
    /// <summary>
    /// Model for creating a message stream.
    /// </summary>
    public class MessageStreamCreateModel : MessagePatchModel
    {
        /// <summary>
        /// The type of stream to be created.
        /// </summary>
        public MessageStreamType Type { get; set; } = MessageStreamType.Transactional;
    }
}
