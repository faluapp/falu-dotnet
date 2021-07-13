using Falu.Core;
using System.Collections.Generic;

namespace Falu.MessageStreams
{
    /// <summary>
    /// Represents the details about a message stream that can be patched.
    /// </summary>
    public class MessageStreamPatchModel : IHasDescription, IHasMetadata, IHasTags
    {
        /// <summary>
        /// Settings for the stream.
        /// </summary>
        public MessageStreamSettings? Settings { get; set; }

        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }

        /// <inheritdoc/>
        public List<string>? Tags { get; set; }
    }
}
