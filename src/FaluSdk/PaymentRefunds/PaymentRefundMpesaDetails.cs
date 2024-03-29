﻿namespace Falu.PaymentRefunds;

/// <summary>
/// Represents the details for an MPESA payment refund.
/// </summary>
public class PaymentRefundMpesaDetails
{
    /// <summary>
    /// The target business short code
    /// </summary>
    public string? BusinessShortCode { get; set; }

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
}
