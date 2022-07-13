using Falu.Core;

namespace Falu.Evaluations;

/// <summary>Options for filtering and pagination of evaluations.</summary>
public record EvaluationsListOptions : BasicListOptions
{
    /// <summary>Filter options for <see cref="Evaluation.Status"/> property.</summary>
    public List<string>? Status { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("status", Status);
    }
}
