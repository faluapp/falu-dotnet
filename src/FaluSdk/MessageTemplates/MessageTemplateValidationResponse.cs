namespace Falu.MessageTemplates;

/// <summary>
/// Response for validating a template
/// </summary>
public class MessageTemplateValidationResponse
{
    /// <summary>
    /// Using the <see cref="Model"/> the text content that
    /// would be produced by this template when the template
    /// content and model are combined.
    /// </summary>
    public string? Rendered { get; set; }

    /// <summary>
    /// A JSON object structure that will provide information
    /// for all keys found in the template content submitted.
    /// If a <see cref="MessageTemplateValidationRequest.Model"/> was submitted, it will be merged
    /// and returned with this model.
    /// </summary>
    public MessageTemplateModel? Model { get; set; }

    /// <summary>
    /// Translations.
    /// Each key must be a three-letter
    /// <see href="https://www.iso.org/iso-639-language-codes.html">ISO language code</see>, in lowercase.
    /// </summary>
    public Dictionary<string, MessageTemplateValidationResponseTranslation> Translations { get; set; } = [];
}
