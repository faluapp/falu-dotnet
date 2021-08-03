using System.Collections.Generic;

namespace Falu.Infrastructure
{
    /// <summary>
    /// A representation of an error.
    /// </summary>
    public class FaluError
    {
        /// <summary>
        /// A short, human-readable summary of the problem type.It SHOULD NOT change from occurrence to occurrence
        /// of the problem, except for purposes of localization(e.g., using proactive content negotiation;
        /// see[RFC7231], Section 3.4).
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// A human-readable explanation specific to this occurrence of the problem.
        /// </summary>
        public virtual string? Detail { get; set; }

        /// <summary>
        /// Gets the validation errors associated the problem.
        /// </summary>
        public virtual IDictionary<string, string[]>? Errors { get; set; }
    }
}
