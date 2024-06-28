using Falu.Core;
using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.Payments;

/// <summary>
/// A model representing details that can be changed about a payment.
/// </summary>
public class PaymentUpdateOptions : IHasOptionalDescription, IHasOptionalMetadata
{
    private Optional<string?>? description;
    private Optional<Dictionary<string, string>?>? metadata;

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Description { get => description; set => description = new(value); }

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<Dictionary<string, string>?>))]
    public Optional<Dictionary<string, string>?>? Metadata { get => metadata; set => metadata = new(value); }
}
