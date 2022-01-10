using Falu.Core;

namespace Falu.Files;

/// <summary>A file on Falu's servers.</summary>
public class File : IHasId, IHasCreated, IHasUpdated, IHasDescription, IHasWorkspaceId, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <inheritdoc/>
    public string? Description { get; set; }

    /// <summary>Purpose of the file.</summary>
    public string? Purpose { get; set; }

    /// <summary>Type of file.</summary>
    /// <example>image/png</example>
    public string? Type { get; set; }

    /// <summary>A name of the file suitable for saving to a filesystem.</summary>
    public string? Filename { get; set; }

    /// <summary>Size in bytes of the file.</summary>
    public long Size { get; set; }

    /// <summary>Time at which the file expires.</summary>
    public DateTimeOffset? Expires { get; set; }

    /// <inheritdoc/>
    public string? WorkspaceId { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
