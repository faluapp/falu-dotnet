namespace Falu.Transfers;

/// <summary>
/// Information for initiating an outgoing payment to customer via MPESA.
/// </summary>
public class TransferCreateOptionsMpesaToCustomer
{
    /// <summary>
    /// The business short code to be debited.
    /// This code must be configured in the workspace.
    /// When not provided, either the default outgoing business code
    /// or the first business code for the workspace is used.
    /// </summary>
    public string? Source { get; set; }

    /// <summary>
    /// The phone number to which the money is to be sent.
    /// </summary>
    public string? Phone { get; set; }
}
