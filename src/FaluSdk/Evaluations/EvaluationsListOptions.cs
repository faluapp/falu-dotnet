using Falu.Core;
using Falu.Infrastructure;
using System.Collections.Generic;

namespace Falu.Evaluations
{
    /// <summary>Options for filtering and pagination of evaluations.</summary>
    public record EvaluationsListOptions : BasicListOptions
    {
        /// <summary>Email address of the evaluations.</summary>
        public string? Email { get; set; }

        /// <summary>Phone number of the evaluations.</summary>
        public string? Phone { get; set; }

        /// <summary>Filter options for <see cref="Evaluation.Status"/> property.</summary>
        public List<EvaluationStatus>? Status { get; set; }

        /// <inheritdoc/>
        internal override void Populate(QueryValues values)
        {
            base.Populate(values);
            values.Add("email", Email)
                  .Add("phone", Phone)
                  .Add("status", Status);
        }
    }
}
