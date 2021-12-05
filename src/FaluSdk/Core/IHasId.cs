namespace Falu.Core;

/// <summary>
/// Interface that identifies objects with an <c>Id</c> property.
/// </summary>
public interface IHasId
{
    /// <summary>
    /// Unique identifier for the object.
    /// </summary>
    public string? Id { get; set; }
}
