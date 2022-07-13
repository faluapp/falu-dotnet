using Falu.Core;

namespace Falu.EvaluationReports;

///
public class EvaluationReportStatement : AbstractEvaluationReport
{
    /// <summary>Provider of the uploaded document.</summary>
    public string? Provider { get; set; }

    /// <summary>
    /// Identifier of the file holding the uploaded document used.
    /// </summary>
    public string? Document { get; set; }

    /// <summary>
    /// Password for the uploaded document.
    /// Present for password-protected files.
    /// </summary>
    public string? Password { get; set; }

    /// <summary>Email of the owner as it appears in the document.</summary>
    public string? Email { get; set; }

    /// <summary>Name of the owner as it appears in the document.</summary>
    public string? Name { get; set; }

    /// <summary>Phone number of the owner as it appears in the document.</summary>
    public string? Phone { get; set; }

    /// <summary>Time at which the document was generated.</summary>
    public DateTimeOffset? Generated { get; set; }

    /// <summary>Period for which the uploaded document covers.</summary>
    public Period? Period { get; set; }

    /// <summary>
    /// Risk probability. The higher the value, the higher the risk
    /// </summary>
    public float? Risk { get; set; }

    /// <summary>
    /// Limit advised for lending in the smallest currency unit.
    /// </summary>
    public long? Limit { get; set; }

    /// <summary>
    /// Time till when the score is deemed valid.
    /// </summary>
    public DateTimeOffset? Expires { get; set; }
}
