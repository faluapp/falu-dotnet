using Falu.Core;

namespace Falu.TemporaryKeys;

/// <summary>
/// Represents a temporary key.
/// </summary>
public class TemporaryKey : IHasId, IHasCreated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// Unique identifiers of the objects that can be accessed using the key.
    /// </summary>
    public List<string> Objects { get; set; } = new List<string>();

    /// <inheritdoc/>
    public DateTimeOffset Expires { get; set; }

    /// <summary>Value provided for authentication secret.</summary>
    public string? Secret { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
