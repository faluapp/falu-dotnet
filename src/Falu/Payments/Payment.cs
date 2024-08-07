﻿using Falu.Core;

namespace Falu.Payments;

/// <summary>
/// Represents a transaction done by a customer to the business.
/// </summary>
public class Payment : IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasDescription, IHasMetadata, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <summary>
    /// Amount of the payment in smallest currency unit.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Status of the payment
    /// </summary>
    public string? Status { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Time at which the payment succeeded. Only populated when successful.
    /// </summary>
    public DateTimeOffset? Succeeded { get; set; }

    /// <summary>
    /// Identifier of the Customer this Payment belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

    /// <summary>
    /// Identifier of the authorization, if the payment passed through a flow requiring authorization.
    /// </summary>
    public string? Authorization { get; set; }

    /// <summary>
    /// The type of the Payment.
    /// An additional property is populated on the Payment with a name matching this value.
    /// It contains additional information specific to the Payment type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// If this is an MPESA Payment, this contains details about the MPESA payment.
    /// </summary>
    public PaymentMpesaDetails? Mpesa { get; set; }

    /// <summary>
    /// Details on the payment error.
    /// Present when in failed state.
    /// </summary>
    public ObjectError? Error { get; set; }

    /// <summary>
    /// Identifier of the refund, if payment has been refunded.
    /// </summary>
    public string? Refund { get; set; }

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
