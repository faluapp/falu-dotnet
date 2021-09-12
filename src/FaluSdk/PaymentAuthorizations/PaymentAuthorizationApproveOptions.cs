using Falu.Core;
using System.Collections.Generic;

namespace Falu.PaymentAuthorizations
{
    /// <summary>
    /// Represents the options for approving a payment authorization.
    /// </summary>
    public class PaymentAuthorizationApproveOptions : IHasMetadata
    {
        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }
    }
}
