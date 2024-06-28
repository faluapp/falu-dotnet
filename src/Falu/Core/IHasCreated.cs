namespace Falu.Core;

/// <summary>
/// Interface that identifies objects with a <c>Created</c> property.
/// </summary>
public interface IHasCreated
{
    /// <summary>
    /// Time at which the object was created.
    /// </summary>
    public DateTimeOffset Created { get; set; }
}
