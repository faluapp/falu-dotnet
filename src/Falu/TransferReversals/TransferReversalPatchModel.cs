using Falu.Core;
using System.Collections.Generic;

namespace Falu.TransferReversals
{
    /// <summary>
    /// A model representing details that can be changed about a transfer reversal.
    /// </summary>
    public class TransferReversalPatchModel : IHasDescription, IHasMetadata, IHasTags
    {
        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }

        /// <inheritdoc/>
        [System.Obsolete(MessageStrings.TagsDeprecated)]
        public List<string>? Tags { get; set; }
    }
}
