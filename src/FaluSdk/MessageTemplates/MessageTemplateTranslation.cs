namespace Falu.MessageTemplates;

/// <summary>
/// A model representing details that can be changed about a template translation.
/// </summary>
public class MessageTemplateTranslation
{
    /// <summary>
    /// Content to use when this template is used to send messages for the given language.
    /// See our template language documentation for more information on the syntax for this field.
    /// </summary>
    public string? Body { get; set; }
}
