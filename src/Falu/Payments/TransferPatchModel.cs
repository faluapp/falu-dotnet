using Falu.Core;
using System.Collections.Generic;

namespace Falu.Payments
{
    /// <summary>
    /// A model representing details that can be changed about a transfer.
    /// </summary>
    public class TransferPatchModel : IHasDescription, IHasMetadata, IHasTags
    {
        /// <inheritdoc/>
        public string Description { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string> Metadata { get; set; }

        /// <inheritdoc/>
        public List<string> Tags { get; set; } = new List<string> { };
    }
}
