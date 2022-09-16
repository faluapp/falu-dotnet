namespace Falu.EvaluationReports;

///
public abstract class AbstractEvaluationReport
{
    /// <summary>
    /// Details on the evaluation error.
    /// Present when not completed.
    /// </summary>
    public EvaluationReportError? Error { get; set; }

    /// <summary>
    /// Whether the check resulted in a successful evaluation.
    /// </summary>
    public bool Completed { get; set; }
}
