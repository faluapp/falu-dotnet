namespace Falu.TransferReversals
{
    /// <summary>
    /// The reason for reversing a transfer.
    /// </summary>
    public enum TransferReversalReason
    {
        ///
        Duplicate,

        ///
        Fraudulent,

        ///
        CustomerRequested,

        ///
        Other,
    }
}
