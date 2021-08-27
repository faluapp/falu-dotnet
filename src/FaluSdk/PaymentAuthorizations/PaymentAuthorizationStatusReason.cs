namespace Falu.PaymentAuthorizations
{
    /// <summary>
    /// Reason for a given status of payment authorization.
    /// </summary>
    public enum PaymentAuthorizationStatusReason
    {
        /// <summary>
        /// No authorization webhook endpoint is set.
        /// </summary>
        Default,

        /// <summary>
        /// Authorization webhook endpoint is invalid.
        /// </summary>
        Invalid,

        /// <summary>
        /// Authorization webhook endpoint was used to approve or decline.
        /// </summary>
        Realtime,

        /// <summary>
        /// Synchronous webhook delivery to the authorization webhook endpoint failed.
        /// </summary>
        Failed,
    }

}
