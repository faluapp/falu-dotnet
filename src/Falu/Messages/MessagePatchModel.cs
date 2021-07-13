using Falu.Core;
using System.Collections.Generic;

namespace Falu.Messages
{
    /// <summary>
    /// A model representing details that can be changed about a message.
    /// </summary>
    public class MessagePatchModel : IHasMetadata, IHasTags
    {
        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }

        /// <inheritdoc/>
        public List<string>? Tags { get; set; }
    }
}
