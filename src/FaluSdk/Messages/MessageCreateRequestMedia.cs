namespace Falu.Messages;

///
public class MessageCreateRequestMedia
{
    /// <summary>
    /// Publicly accessible URL for the media file.
    /// Required if <see cref="File"/> is not specified.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Unique identifier of the file containing the media file.
    /// Required if <see cref="Url"/> is not specified.
    /// </summary>
    public string? File { get; set; }
}
