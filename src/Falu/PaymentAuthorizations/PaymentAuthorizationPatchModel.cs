using Falu.Core;
using System.Collections.Generic;

namespace Falu.PaymentAuthorizations
{
    /// <summary>
    /// Represents the details that can be patched in a payment authorization.
    /// </summary>
    public class PaymentAuthorizationPatchModel : IHasMetadata, IHasTags
    {
        /// <inheritdoc/>
        public Dictionary<string, string>? Metadata { get; set; }

        /// <inheritdoc/>
        public List<string>? Tags { get; set; }
    }
}
