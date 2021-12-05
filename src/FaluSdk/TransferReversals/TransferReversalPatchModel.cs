using Falu.Core;

namespace Falu.TransferReversals
{
    /// <summary>
    /// A model representing details that can be changed about a transfer reversal.
    /// </summary>
    public class TransferReversalPatchModel : IHasDescription, IHasMetadata
    {
        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }
    }
}
