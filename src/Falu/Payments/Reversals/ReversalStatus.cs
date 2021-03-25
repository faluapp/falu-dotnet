namespace Falu.Payments.Reversals
{
    /// <summary>
    /// The status of a reversal.
    /// </summary>
    public enum ReversalStatus
    {
        ///
        Pending,

        ///
        InTransit,

        ///
        Succeeded,

        ///
        Failed,
    }
}
