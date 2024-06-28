using Falu.Core;
using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.PaymentAuthorizations;

/// <summary>
/// Represents the details that can be patched in a payment authorization.
/// </summary>
public class PaymentAuthorizationUpdateOptions : IHasOptionalMetadata
{
    private Optional<Dictionary<string, string>?>? metadata;

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<Dictionary<string, string>?>))]
    public Optional<Dictionary<string, string>?>? Metadata { get => metadata; set => metadata = new(value); }
}
