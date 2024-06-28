using Falu.Core;

namespace Falu.FileLinks;

/// <summary>A model representing details that can be changed about a file link.</summary>
public class FileLinkUpdateOptions : IHasMetadata
{
    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <summary>Time at which the link expires.</summary>
    public DateTimeOffset? Expires { get; set; }
}
