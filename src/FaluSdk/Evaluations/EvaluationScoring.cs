namespace Falu.Evaluations;

/// <summary>
/// Represents the scoring done for an evaluation
/// </summary>
[Obsolete("Use EvaluationScoringOutputs instead")]
public class EvaluationScoring
{
    /// <summary>
    /// Risk probability. The higher the value, the higher the risk.
    /// Ranges: 0.0o to 1.00
    /// </summary>
    public float? Risk { get; set; }

    /// <summary>
    /// Limit advised for lending in the smallest curency unit.
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// Time up to which the score is deemed valid.
    /// </summary>
    public DateTimeOffset Expires { get; set; }
}
