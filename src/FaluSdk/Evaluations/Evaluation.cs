using Falu.Core;

namespace Falu.Evaluations;

/// <summary>
/// An evaluation record.
/// </summary>
public class Evaluation : EvaluationPatchModel, IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasWorkspaceId, IHasLive, IHasEtag
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
    /// Statement used for the evaluation.
    /// </summary>
    public Statement? Statement { get; set; }

    /// <summary>
    /// Scoring generated for the evaluation.
    /// Only populated if extraction succeeded.
    /// </summary>
    public EvaluationScoring? Scoring { get; set; }

    /// <inheritdoc/>
    public string? WorkspaceId { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
