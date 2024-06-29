using Falu.Core;
using Falu.Serialization;
using System.Text.Json.Serialization;

namespace Falu.MessageTemplates;

/// <summary>
/// A model representing details that can be changed about a template
/// </summary>
public class MessageTemplateUpdateOptions : IHasOptionalDescription, IHasOptionalMetadata
{
    private Optional<string?>? alias;
    private Optional<string?>? body;
    private Optional<string?>? description;
    private Optional<Dictionary<string, string>?>? metadata;
    private Optional<Dictionary<string, MessageTemplateTranslation>?>? translations;

    /// <summary>
    /// An optional string you can provide to identify this template.
    /// Allowed characters are numbers, ASCII letters, and ‘.’, ‘-’, ‘_’ characters,
    /// and the string has to start with a letter.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Alias { get => alias; set => alias = new(value); }

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Description { get => description; set => description = new(value); }

    /// <summary>
    /// The content to use when this template is used to send messages.
    /// See our template language documentation for more information on the syntax for this field.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<string?>))]
    public Optional<string?>? Body { get => body; set => body = new(value); }

    /// <summary>
    /// Translations for the template.
    /// Each key must be a three-letter
    /// <see href="https://www.iso.org/iso-639-language-codes.html">ISO language code</see>, in lowercase.
    /// </summary>
    [JsonConverter(typeof(OptionalConverter<Dictionary<string, MessageTemplateTranslation>>))]
    public Optional<Dictionary<string, MessageTemplateTranslation>?>? Translations { get => translations; set => translations = new(value); }

    /// <inheritdoc/>
    [JsonConverter(typeof(OptionalConverter<Dictionary<string, string>?>))]
    public Optional<Dictionary<string, string>?>? Metadata { get => metadata; set => metadata = new(value); }
}
