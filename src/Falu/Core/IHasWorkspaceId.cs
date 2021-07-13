namespace Falu.Core
{
    /// <summary>
    /// Interface that identifies objects with a <c>WorkspaceId</c> property.
    /// </summary>
    public interface IHasWorkspaceId
    {
        /// <summary>
        /// Unique identifier for the workspace that the object belongs to.
        /// </summary>
        string? WorkspaceId { get; set; }
    }
}
