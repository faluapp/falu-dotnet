using Falu.Core;

namespace Falu.EvaluationReports;

/// <summary>Options for filtering and pagination of evaluation reports.</summary>
public record EvaluationReportsListOptions : BasicListOptions
{
    /// <summary>
    /// Unique identifier of the evaluation to filter for.
    /// </summary>
    public string? Evaluation { get; set; }

    /// <inheritdoc/>
    internal override void Populate(QueryValues values)
    {
        base.Populate(values);
        values.Add("evaluation", Evaluation);
    }
}
