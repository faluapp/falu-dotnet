using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.Core;

// TODO: replace this file when migration to JSON MergePatch is complete
///
public partial class TestingUpdateOptions
{
    private Optional<bool?>? @default;
    private Optional<string?>? status;

    ///
    [JsonConverter(typeof(OptionalConverter<bool?>))]
    public Optional<bool?>? Default { get => @default; set => @default = new(value); }

    ///
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Status { get => status; set => status = new(value); }
}
