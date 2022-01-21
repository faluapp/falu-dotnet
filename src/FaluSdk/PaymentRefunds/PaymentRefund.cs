using Falu.Core;

namespace Falu.PaymentRefunds;

/// <summary>
/// Represents a reversal of a Payment.
/// </summary>
public class PaymentRefund : PaymentRefundPatchModel, IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

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

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Identifier of the Payment reversed.
    /// </summary>
    public string? Payment { get; set; }

    /// <summary>
    /// Time at which the reversal succeeded. Only populated when successful.
    /// </summary>
    public DateTimeOffset? Succeeded { get; set; }

    /// <summary>
    /// Details of the reversal if done via MPESA.
    /// Only populated if the payment being reversed used an MPESA instrument.
    /// </summary>
    public PaymentRefundMpesaDetails? Mpesa { get; set; }

    /// <summary>
    /// Details about failure if the reversal is in failed state.
    /// </summary>
    public PaymentRefundFailureDetails? Failure { get; set; }

    /// <inheritdoc/>
    public string? WorkspaceId { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }

}
