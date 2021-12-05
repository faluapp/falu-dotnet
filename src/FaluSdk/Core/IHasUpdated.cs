namespace Falu.Core
{
    /// <summary>
    /// Interface that identifies objects with an <c>Updated</c> property.
    /// </summary>
    public interface IHasUpdated
    {
        /// <summary>
        /// Time at which the object was last updated.
        /// </summary>
        public DateTimeOffset Updated { get; set; }
    }
}
