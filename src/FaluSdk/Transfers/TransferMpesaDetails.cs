﻿namespace Falu.Transfers;

/// <summary>
/// Represents the provider details for a MPESA transfer.
/// </summary>
public class TransferMpesaDetails
{
    /// <summary>
    /// The target business short code
    /// </summary>
    public string? BusinessShortCode { get; set; }

    /// <summary>
    /// Destination of where the transfer is/was sent to.
    /// </summary>
    public string? Destination { get; set; }

    /// <summary>
    /// Unique identifier for request as issued by MPESA.
    /// Only populated for flows that initiate the transaction instead of MPESA.
    /// The value is only available after the request is sent to MPESA.
    /// </summary>
    public string? RequestId { get; set; }

    /// <summary>
    /// Unique transaction identifier generated by MPESA.
    /// Only populated for completed transactions.
    /// </summary>
    public string? Receipt { get; set; }

    /// <summary>
    /// Charges for the transaction in the smallest currency unit.
    /// </summary>
    public long? Charges { get; set; }

    /// <summary>
    /// Details of the receiver.
    /// </summary>
    public string? Receiver { get; set; }
}
