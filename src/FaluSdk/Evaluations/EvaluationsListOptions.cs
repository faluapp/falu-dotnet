using Falu.Core;
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
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);

            dictionary.AddIfNotNull("email", Email)
                      .AddIfNotNull("phone", Phone)
                      .AddIfNotNull("status", Status, ConvertEnumList);

            return dictionary;
        }
    }
}
