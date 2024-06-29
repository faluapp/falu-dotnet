using Falu.Core;
using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.Messages;

/// <summary>
/// A model representing details that can be changed about a message.
/// </summary>
public class MessageUpdateOptions : IHasOptionalMetadata
{
    private Optional<Dictionary<string, string>?>? metadata;

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<Dictionary<string, string>?>))]
    public Optional<Dictionary<string, string>?>? Metadata { get => metadata; set => metadata = new(value); }
}
