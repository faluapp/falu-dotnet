using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.Core;

// TODO: replace this file when migration to JSON MergePatch is complete
///
public partial class TestingUpdateOptions
{
    ///
    [JsonPropertyName("default")]
    [JsonConverter(typeof(OptionalConverter<bool>))]
    public Optional<bool>? Default { get; set; }

    ///
    [JsonPropertyName("status")]
    [JsonConverter(typeof(OptionalConverter<string>))]
    public Optional<string>? Status { get; set; }
}
