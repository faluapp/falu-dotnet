using Falu.Core;
using System;

namespace Falu.Evaluations
{
    /// <summary>
    /// An evaluation record.
    /// </summary>
    public class Evaluation : EvaluationPatchModel, IHasId, IHasCreated, IHasUpdated, IHasLive, IHasEtag
    {
        /// <inheritdoc/>
        public string Id { get; set; }

        /// <summary>
        /// Three-letter <see href="https://www.iso.org/iso-4217-currency-codes.html">ISO currency code</see>,
        /// in lowercase.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Scope of the evaluation.
        /// </summary>
        public EvaluationScope Scope { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Created { get; set; }

        /// <inheritdoc/>
        public DateTimeOffset Updated { get; set; }

        /// <summary>
        /// Status of the evaluation.
        /// </summary>
        public EvaluationStatus Status { get; set; }

        /// <summary>
        /// Statement used for the evaluation.
        /// </summary>
        public Statement Statement { get; set; }

        /// <summary>
        /// Scoring generated for the evaluation.
        /// Only populated if extraction succeeded.
        /// </summary>
        public EvaluationScoring Scoring { get; set; }

        /// <inheritdoc/>
        public bool Live { get; set; }

        /// <inheritdoc/>
        public string Etag { get; set; }
    }
}
