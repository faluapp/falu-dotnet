using System;

namespace Falu.Evaluations
{
    /// <summary>
    /// Represents the scoring done for an evaluation
    /// </summary>
    public class EvaluationScoring
    {
        /// <summary>
        /// Risk probability. The higher the value, the higher the risk.
        /// Ranges: 0.0o to 1.00
        /// </summary>
        public float? Risk { get; set; }

        /// <summary>
        /// Limit advised for lending.
        /// </summary>
        public int? Limit { get; set; }

        /// <summary>
        /// Time upto which the records scoring is deemed valid.
        /// </summary>
        public DateTimeOffset Expires { get; set; }
    }
}
