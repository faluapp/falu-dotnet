namespace Falu.Core;

/// <summary>
/// Interface that identifies objects with a <c>WorkspaceId</c> property.
/// </summary>
public interface IHasWorkspace
{
    /// <summary>
    /// Unique identifier for the workspace that the object belongs to.
    /// </summary>
    string? Workspace { get; set; }
}
