using System.Text.Json.Nodes;

namespace Falu.Messages;

/// <summary>
/// Information about the template to be used to send a message.
/// </summary>
public class MessageCreateRequestTemplate
{
    /// <summary>
    /// Unique identifier of the template used.
    /// </summary>
    public string? Id { get; set; }

    /// <summary>
    /// Alias of the template used.
    /// </summary>
    public string? Alias { get; set; }

    /// <summary>
    /// Model applied when rending the template.
    /// </summary>
    /// <remarks>
    /// For convenience, use <see cref="MessageTemplates.MessageTemplateModel"/> for example:
    /// <br/>
    /// <c>Model = MessageTemplateModel.Create(new { otp = "123" })</c>
    /// </remarks>
    public JsonObject? Model { get; set; }
}
