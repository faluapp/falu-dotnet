namespace Falu.Core;

/// <summary>
/// Interface that identifies objects with an <c>Description</c> property.
/// </summary>
public interface IHasDescription
{
    /// <summary>
    /// An arbitrary string attached to the object. Often useful for displaying to users.
    /// </summary>
    public string? Description { get; set; }
}
