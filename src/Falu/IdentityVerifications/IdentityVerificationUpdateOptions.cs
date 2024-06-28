using Falu.Core;
using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.IdentityVerifications;

/// <summary>
/// A model representing details that can be changed about an identity verification.
/// </summary>
public class IdentityVerificationUpdateOptions : IHasOptionalDescription, IHasOptionalMetadata
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
