namespace Falu.FileLinks;

/// <summary>Information for creating a file link.</summary>
public class FileLinkCreateRequest : FileLinkPatchModel
{
    /// <summary>Unique identifier of the file.</summary>
    public string? FileId { get; set; }
}
