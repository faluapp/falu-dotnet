using Falu.Core;
using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.FileLinks;

/// <summary>A model representing details that can be changed about a file link.</summary>
public class FileLinkUpdateOptions : IHasOptionalMetadata
{
    private Optional<DateTimeOffset?>? expires;
    private Optional<Dictionary<string, string>?>? metadata;

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<Dictionary<string, string>?>))]
    public Optional<Dictionary<string, string>?>? Metadata { get => metadata; set => metadata = new(value); }

    /// <summary>Time at which the link expires.</summary>
    [JsonConverter(typeof(OptionalConverter<DateTimeOffset?>))]
    public Optional<DateTimeOffset?>? Expires { get => expires; set => expires = new(value); }
}
