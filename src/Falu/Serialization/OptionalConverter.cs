using Falu.Core;
using System.Text.Json;
using System.Text.Json.Serialization;
using SC = Falu.Serialization.FaluSerializerContext;

namespace Falu.Serialization;

/// <summary>JSON converter for <see cref="Optional{T}"/>.</summary>
public sealed class OptionalConverter<T> : JsonConverter<Optional<T>>
{
    /// <inheritdoc/>
    public override Optional<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return default;

        var value = JsonSerializer.Deserialize(ref reader, SC.Default.GetRequiredTypeInfo<T>());
        return new Optional<T>(value);
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, Optional<T> value, JsonSerializerOptions options)
    {
        var inner = value.Value;
        if (!value.HasValue || inner is null)
        {
            writer.WriteNullValue();
            return;
        }

        JsonSerializer.Serialize(writer, inner, SC.Default.GetRequiredTypeInfo<T>());
    }
}
