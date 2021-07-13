using Falu.Core;
using System.Collections.Generic;

namespace Falu.Evaluations
{
    /// <summary>
    /// A model representing details that can be changed about an evaluation.
    /// </summary>
    public class EvaluationPatchModel : IHasDescription, IHasMetadata, IHasTags
    {
        /// <inheritdoc/>
        public string? Description { get; set; }

        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }

        /// <inheritdoc/>
        public List<string>? Tags { get; set; }
    }
}
