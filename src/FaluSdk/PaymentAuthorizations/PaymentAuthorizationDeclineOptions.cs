using Falu.Core;

namespace Falu.PaymentAuthorizations;

/// <summary>
/// Represents the options for declining a payment authorization.
/// </summary>
public class PaymentAuthorizationDeclineOptions : IHasMetadata
{
    /// <summary>
    /// Reason for declining the payment authorization.
    /// Defaults to <c>other</c>
    /// </summary>
    public string? DeclineReason { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }
}
