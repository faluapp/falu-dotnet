namespace Falu.Core
{
    /// <summary>
    /// Interface that identifies objects with an <c>Live</c> property.
    /// </summary>
    public interface IHasLive
    {
        /// <summary>
        /// Indicates if this record belongs in the live environment
        /// </summary>
        public bool Live { get; set; }
    }
}
