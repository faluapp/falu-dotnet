namespace Falu.MessageTemplates;

/// <summary>
/// Model for requesting template validation
/// </summary>
public class MessageTemplateValidationRequest
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
    /// The template model to be used when rendering test content.
    /// </summary>
    public MessageTemplateModel? Model { get; set; }
}
