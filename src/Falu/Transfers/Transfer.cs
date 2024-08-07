﻿using Falu.Core;

namespace Falu.Transfers;

/// <summary>
/// Represents a transaction made by the business to customer or another business.
/// </summary>
public class Transfer : IHasId, IHasCurrency, IHasCreated, IHasUpdated, IHasDescription, IHasMetadata, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public string? Currency { get; set; }

    /// <summary>
    /// Amount of the transfer in smallest currency unit.
    /// </summary>
    public long Amount { get; set; }

    /// <summary>
    /// Status of the transfer
    /// </summary>
    public string? Status { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Time at which the transfer succeeded. Only populated when successful.
    /// </summary>
    public DateTimeOffset? Succeeded { get; set; }

    /// <summary>
    /// The type of the Transfer.
    /// An additional property is populated on the Transfer with a name matching this value.
    /// It contains additional information specific to the Transfer type.
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Purpose of the transfer.
    /// </summary>
    public string? Purpose { get; set; }

    /// <summary>
    /// Identifier of the Customer this Transfer belongs to, if one exists.
    /// </summary>
    public string? Customer { get; set; }

    /// <summary>
    /// If this is an MPESA transfer, this contains details about the MPESA transfer.
    /// </summary>
    public TransferMpesaDetails? Mpesa { get; set; }

    /// <summary>
    /// Details on the transfer error.
    /// Present when in failed state.
    /// </summary>
    public ObjectError? Error { get; set; }

    /// <summary>
    /// Identifier of the reversal, if transfer has been reversed.
    /// </summary>
    public string? Reversal { get; set; }

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
