using Falu.Core;

namespace Falu.PaymentAuthorizations
{
    /// <summary>
    /// Represents the options for declining a payment authorization.
    /// </summary>
    public class PaymentAuthorizationDeclineOptions : IHasMetadata
    {
        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }
    }
}
