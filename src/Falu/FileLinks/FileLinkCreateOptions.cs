using Falu.Core;

namespace Falu.FileLinks;

/// <summary>Information for creating a file link.</summary>
public class FileLinkCreateOptions : IHasMetadata
{
    /// <summary>Unique identifier of the file.</summary>
    public string? File { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>Time at which the link expires.</summary>
    public DateTimeOffset? Expires { get; set; }
}
