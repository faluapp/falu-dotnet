using Falu.Core;

namespace Falu.Evaluations;

/// <summary>An evaluation report.</summary>
public class EvaluationReport : IHasId, IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Identifier of the evaluation that created this report.
    /// </summary>
    public string? Evaluation { get; set; }

    /// <summary>
    /// The options that initiated this report.
    /// </summary>
    public EvaluationScoringOptions? Options { get; set; }

    /// <summary>
    /// Details on the user’s acceptance of the Services Agreement.
    /// </summary>
    public EvaluationReportConsent? Consent { get; set; }

    /// <summary>
    /// Result from a bureau in the relevant jurisdiction.
    /// </summary>
    public EvaluationReportBureau? Bureau { get; set; }

    /// <summary>
    /// Result from a financial statement.
    /// </summary>
    public EvaluationReportStatement? Statement { get; set; }

    /// <summary>
    /// Result from an anti-money laundering check.
    /// </summary>
    public EvaluationReportAml? Aml { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}

///
public class EvaluationReportConsent
{
    /// <summary>
    /// Time at which the user gave consent for the evaluation to be done.
    /// </summary>
    public DateTimeOffset Date { get; set; }

    /// <summary>
    /// IP address from which the user gave consent for the evaluation to be done.
    /// </summary>
    public string? IP { get; set; }

    /// <summary>
    /// User agent of the device (e.g. browser) from which the user gave consent for the evaluation to be done.
    /// </summary>
    public string? UserAgent { get; set; }
}

///
public class EvaluationReportBureau : AbstractEvaluationReport { }

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

///
public class EvaluationReportAml : AbstractEvaluationReport { }

///
public abstract class AbstractEvaluationReport
{
    /// <summary>
    /// Details on the evalution error.
    /// Present when not completed.
    /// </summary>
    public EvaluationReportError? Error { get; set; }

    /// <summary>
    /// Whether the check resulted in a successful evaluation.
    /// </summary>
    public bool Completed { get; set; }
}

///
public class EvaluationReportError
{
    /// <summary>
    /// A short machine-readable string giving the reason for the evaluation failure.
    /// </summary>
    public string? Code { get; set; }

    /// <summary>
    /// A human-readable message giving the reason for the failure.
    /// These message can be shown to your user.
    /// </summary>
    public string? Description { get; set; }
}
