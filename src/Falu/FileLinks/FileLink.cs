﻿using Falu.Core;

namespace Falu.FileLinks;

/// <summary>A file link.</summary>
public class FileLink : IHasId, IHasCreated, IHasUpdated, IHasMetadata, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>Unique identifier of the file.</summary>
    public string? File { get; set; }

    /// <summary>Publicly accessible URL to download the file.</summary>
    public string? Url { get; set; }

    /// <summary>Whether this link is already expired.</summary>
    public bool Expired { get; set; }

    /// <summary>Time at which the link expires.</summary>
    public DateTimeOffset? Expires { get; set; }

    /// <inheritdoc/>
    public Dictionary<string, string>? Metadata { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
