using Falu.Core;

namespace Falu.PaymentRefunds;

/// <summary>
/// Represents a reversal of a Payment.
/// </summary>
public class PaymentRefund : IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasDescription, IHasMetadata, IHasWorkspace, IHasLive, IHasEtag
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
    /// Identifier of the Customer this Payment Refund belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

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
    /// Details on the payment refund error.
    /// Present when in failed state.
    /// </summary>
    public ObjectError? Error { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
