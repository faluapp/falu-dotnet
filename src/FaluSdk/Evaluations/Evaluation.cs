using Falu.Core;

namespace Falu.Evaluations;

/// <summary>An evaluation record.</summary>
public class Evaluation : EvaluationPatchModel, IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasRedaction, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Status of the evaluation.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// A set of options for the evaluation's process.
    /// </summary>
    public EvaluationOptions? Options { get; set; }

    /// <summary>
    /// The short-lived client secret used by front-end libraries to show an evaluation modal inside your app.
    /// This client secret expires after 24 hours and can only be used once.
    /// Don’t store it, log it, embed it in a URL, or expose it to anyone other than the user.
    /// Make sure that you have TLS enabled on any page that includes the client secret.
    /// </summary>
    public string? ClientSecret { get; set; }

    /// <summary>
    /// The short-lived URL that you use to redirect a user to Falu to submit their evaluation information.
    /// This link expires after 24 hours and can only be used once.
    /// Don’t store it, log it, send it in emails or expose it to anyone other than the target user.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Unique identifiers of the reports for this verification.
    /// </summary>
    public List<string> Reports { get; set; } = new List<string>();

    /// <summary>
    /// If present, this property tells you the last error encountered when processing the evaluation.
    /// </summary>
    public EvaluationLastError? Error { get; set; }

    /// <summary>
    /// The evaluations and scoring output data.
    /// </summary>
    public EvaluationOutputs? Outputs { get; set; }

    /// <inheritdoc/>
    public DataRedaction? Redaction { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
