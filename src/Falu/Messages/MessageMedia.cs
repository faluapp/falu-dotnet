namespace Falu.Messages;

///
public class MessageMedia
{
    /// <summary>
    /// Publicly accessible URL for the media file.
    /// Present if <see cref="File"/> is not specified.
    /// </summary>
    public string? Url { get; set; }

    /// <summary>
    /// Unique identifier of the file containing the media file, if any.
    /// Present if <see cref="Url"/> is not specified.
    /// </summary>
    public string? File { get; set; }

    /// <summary>Type of media.</summary>
    public string? Type { get; set; }

    /// <summary>Size in bytes of the media.</summary>
    public long Size { get; set; }
}
