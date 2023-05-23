using Falu.MessageTemplates;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Falu.Serialization;

/// <summary>JSON converter for <see cref="MessageTemplateModel"/>.</summary>
public class MessageTemplateModelJsonConverter : JsonConverter<MessageTemplateModel>
{
    /// <inheritdoc/>
    public override MessageTemplateModel Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null) return default;
        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new InvalidOperationException("Only objects are supported");
        }

        var nodeOptions = new JsonNodeOptions { PropertyNameCaseInsensitive = options.PropertyNameCaseInsensitive };
        var node = JsonNode.Parse(ref reader, nodeOptions);
        return new MessageTemplateModel(node!.AsObject());
    }

    /// <inheritdoc/>
    public override void Write(Utf8JsonWriter writer, MessageTemplateModel value, JsonSerializerOptions options)
    {
        if (value.Object is null)
        {
            writer.WriteNullValue();
            return;
        }

        value.Object.WriteTo(writer);
    }
}
