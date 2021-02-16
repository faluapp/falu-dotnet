namespace Falu.Messages
{
    /// <summary>
    /// Information about the template used (or to be used) to send a message.
    /// </summary>
    public class MessageTemplate
    {
        /// <summary>
        /// Unique identifier of the template used.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Alias of the template used.
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        /// Model applied when rending the template.
        /// </summary>
        public object Model { get; set; }
    }
}
