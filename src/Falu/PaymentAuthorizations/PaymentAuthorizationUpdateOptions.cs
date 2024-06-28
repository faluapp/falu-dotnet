using Falu.Core;

namespace Falu.PaymentAuthorizations;

/// <summary>
/// Represents the details that can be patched in a payment authorization.
/// </summary>
public class PaymentAuthorizationUpdateOptions : IHasMetadata
{
    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
