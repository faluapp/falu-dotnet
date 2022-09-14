using Falu.Core;

namespace Falu.TransferReversals;

/// <summary>
/// Represents a reversal of a Transfer.
/// </summary>
public class TransferReversal : TransferReversalPatchModel, IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <summary>
    /// Identifier of the Transfer reversed.
    /// </summary>
    public string? Transfer { get; set; }

    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <summary>
    /// Amount reversed in smallest currency unit.
    /// This is pulled from the Payment.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Reason for the reversal.
    /// </summary>
    public string? Reason { get; set; }

    /// <summary>
    /// Status of the reversal.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Identifier of the Customer this Transfer Reversal belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Time at which the reversal succeeded. Only populated when successful.
    /// </summary>
    public DateTimeOffset? Succeeded { get; set; }

    /// <summary>
    /// Details of the reversal if done via MPESA.
    /// Only populated if the payment being reversed used an MPESA instrument.
    /// </summary>
    public TransferReversalMpesaDetails? Mpesa { get; set; }

    /// <summary>
    /// Details about failure if the reversal is in failed state.
    /// </summary>
    public TransferReversalFailureDetails? Failure { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
