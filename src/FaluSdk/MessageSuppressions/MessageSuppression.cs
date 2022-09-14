using Falu.Core;

namespace Falu.MessageSuppressions;

/// <summary>
/// A suppression for a phone number destination used in messaging.
/// </summary>
public class MessageSuppression : IHasId, IHasCreated, IHasUpdated, IHasWorkspace, IHasLive, IHasEtag
{
    /// <inheritdoc/>
    public string? Id { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <inheritdoc/>
    public DateTimeOffset Updated { get; set; }

    /// <summary>
    /// Stream where the suppression exists.
    /// </summary>
    public string? Stream { get; set; }

    /// <summary>
    /// Destination phone number in <see href="https://en.wikipedia.org/wiki/E.164">E.164 format</see>.
    /// </summary>
    public string? To { get; set; }

    /// <summary>
    /// Origin of the suppression.
    /// </summary>
    public string? Origin { get; set; }

    /// <summary>
    /// Reason for the suppression
    /// </summary>
    public string? Reason { get; set; }

    /// <inheritdoc/>
    public string? Workspace { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }

    /// <inheritdoc/>
    public string? Etag { get; set; }
}
