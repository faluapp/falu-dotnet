namespace Falu.FileLinks;

/// <summary>Information for creating a file link.</summary>
public class FileLinkCreateOptions : FileLinkUpdateOptions
{
    /// <summary>Unique identifier of the file.</summary>
    public string? File { get; set; }
}
