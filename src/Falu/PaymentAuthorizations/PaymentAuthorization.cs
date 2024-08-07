﻿using Falu.Core;
using Falu.Payments;

namespace Falu.PaymentAuthorizations;

/// <summary>
/// Represents a payment authorization.
/// </summary>
public class PaymentAuthorization : IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasMetadata, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <summary>
    /// Amount that was authorized or rejected, in smallest currency unit.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Whether the authorization has been approved.
    /// </summary>
    public bool Approved { get; set; }

    /// <summary>
    /// Status of the payment authorization.
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// Reason for the given status of the payment authorization.
    /// </summary>
    public string? StatusReason { get; set; }

    /// <summary>
    /// Reason for declining the payment authorization.
    /// Only populated when the authorization is not approved.
    /// </summary>
    public string? DeclineReason { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Type of the payment requiring authorization.
    /// An additional property is populated on the authorization with a name matching this value.
    /// It contains additional information specific to the payment type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// If this is an MPESA Payment, this contains details about the MPESA payment.
    /// </summary>
    public PaymentMpesaDetails? Mpesa { get; set; }

    /// <summary>
    /// Identifier of the payment created after the authorization is approved and closed.
    /// </summary>
    public string? Payment { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
