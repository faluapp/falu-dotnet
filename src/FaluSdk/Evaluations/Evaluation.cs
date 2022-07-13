using Falu.Core;

namespace Falu.Evaluations;

/// <summary>An evaluation record.</summary>
public class Evaluation : EvaluationPatchModel, IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <summary>
    /// Scope of the evaluation.
    /// </summary>
    public string? Scope { get; set; }

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
    public EvaluationScoringOptions? Options { get; set; }

    /// <summary>
    /// Statement used for the evaluation.
    /// </summary>
    public Statement? Statement { get; set; }

    /// <summary>
    /// The secret to use when generating <see cref="Url"/> and <see cref="ClientSecret"/>.
    /// The values generated are used for authentication on clients and the browser.
    /// Every time the document is updated a new value is generated which is random.
    /// </summary>
    public string? Secret { get; set; }

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
    /// Scoring generated for the evaluation.
    /// Only populated if extraction succeeded.
    /// </summary>
    public EvaluationScoring? Scoring { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
