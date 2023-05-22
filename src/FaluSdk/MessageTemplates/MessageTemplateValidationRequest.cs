using System.Text.Json.Nodes;

namespace Falu.MessageTemplates;

/// <summary>
/// Model for requesting template validation
/// </summary>
public class MessageTemplateValidationRequest
{
    /// <summary>
    /// The content to use when this template is used to send messages.
    /// See our template language documentation for more information on the syntax for this field.
    /// </summary>
    public string? Body { get; set; }

    /// <summary>
    /// The template model to be used when rendering test content.
    /// </summary>
    /// <remarks>
    /// For convenience, use <see cref="MessageTemplateModel"/> for example:
    /// <br/>
    /// <c>Model = MessageTemplateModel.Create(new { otp = "123" })</c>
    /// </remarks>
    public JsonObject? Model { get; set; }
}
