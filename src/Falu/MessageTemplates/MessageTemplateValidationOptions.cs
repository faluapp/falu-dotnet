namespace Falu.MessageTemplates;

/// <summary>
/// Model for requesting template validation
/// </summary>
public class MessageTemplateValidationOptions
{
    /// <summary>
    /// Type of the template.
    /// Defaults to <c>transactional</c>
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// The content to use when this template is used to send messages.
    /// See our template language documentation for more information on the syntax for this field.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// Translations for the template.
    /// Each key must be a three-letter
    /// <see href="https://www.iso.org/iso-639-language-codes.html">ISO language code</see>, in lowercase.
    /// </summary>
    public Dictionary<string, MessageTemplateTranslation> Translations { get; set; } = [];

    /// <summary>
    /// The template model to be used when rendering test content.
    /// </summary>
    public MessageTemplateModel? Model { get; set; }
}
