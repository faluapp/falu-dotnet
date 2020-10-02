namespace Falu.Core
{
    /// <summary>
    /// Interface that identifies objects with an <c>Etag</c> property.
    /// </summary>
    public interface IHasEtag
    {
        /// <summary>
        /// A value that validates concurrent access of this object when stored in the database.
        /// This value changes with every update and can thus be used to track changes.
        /// </summary>
        public string Etag { get; set; }
    }
}
