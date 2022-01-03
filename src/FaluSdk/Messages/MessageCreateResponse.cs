using Falu.Core;

namespace Falu.Messages;

/// <summary>
/// Response from sending one or more messages.
/// </summary>
public class MessageCreateResponse : IHasCreated, IHasWorkspaceId, IHasLive
{
    /// <inheritdoc/>
    public DateTimeOffset Created { get; set; }

    /// <summary>
    /// List of unique identifiers of the messages created.
    /// </summary>
    public IList<string>? Ids { get; set; }

    /// <inheritdoc/>
    public string? WorkspaceId { get; set; }

    /// <inheritdoc/>
    public bool Live { get; set; }
}
