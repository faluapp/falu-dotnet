using Falu.Core;
using System.Collections.Generic;

namespace Falu.PaymentRefunds
{
    /// <summary>
    /// A model representing details that can be changed about a payment reversal.
    /// </summary>
    public class PaymentReversalPatchModel : IHasDescription, IHasMetadata
    {
        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }
    }
}
