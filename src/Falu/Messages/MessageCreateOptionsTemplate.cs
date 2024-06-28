namespace Falu.Messages;

/// <summary>
/// Information about the template to be used to send a message.
/// </summary>
public class MessageCreateOptionsTemplate
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
    /// Three-letter <see href="https://www.iso.org/iso-639-language-codes.html">ISO language code</see>,
    /// in lowercase.
    /// 
    /// This determines the language translation to be used.
    /// When not provided, the default one is used.
    /// </summary>
    public string? Language { get; set; }

    /// <summary>
    /// Model applied when rending the template.
    /// </summary>
    public MessageTemplates.MessageTemplateModel? Model { get; set; }
}
