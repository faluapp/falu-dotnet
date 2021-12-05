namespace Falu.TransferReversals;

/// <summary>
/// Information for creating a transfer reversal.
/// </summary>
public class TransferReversalCreateRequest : TransferReversalPatchModel
{
    /// <summary>
    /// Identifier of the Transfer to reverse.
    /// </summary>
    public string? TransferId { get; set; }

    /// <summary>
    /// Reason for the reversal.
    /// </summary>
    public TransferReversalReason Reason { get; set; }
}
