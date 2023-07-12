namespace Falu.MessageTemplates;

/// <summary>
/// Information for creating a template
/// </summary>
public class MessageTemplateCreateRequest : MessageTemplatePatchModel
{
    /// <summary>
    /// Type of the template.
    /// Defaults to <c>utility</c>
    /// </summary>
    public string? Type { get; set; }
}
