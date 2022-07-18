using Falu.Core;
using Falu.Evaluations;

namespace Falu.EvaluationReports;

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
