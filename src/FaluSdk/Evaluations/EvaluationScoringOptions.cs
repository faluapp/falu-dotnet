namespace Falu.Evaluations;

/// <summary>
/// Options for evaluation scoring
/// </summary>
public class EvaluationScoringOptions
{
    /// <summary>
    /// Scope of the evaluation.
    /// </summary>
    public string? Scope { get; set; }

    /// <summary>
    /// Options for evaluation using a statement.
    /// </summary>
    public EvaluationScoringOptionsForStatement? Statement { get; set; }
}

/// <summary>
/// Evaluation scoring options for using statements.
/// </summary>
public class EvaluationScoringOptionsForStatement
{
    /// <summary>
    /// The allowed providers.
    /// Users will only be allowed to upload documents from these providers.
    /// </summary>
    public List<string>? Allowed { get; set; }
}
