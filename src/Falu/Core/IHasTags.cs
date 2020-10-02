using System.Collections.Generic;

namespace Falu.Core
{
    /// <summary>
    /// Interface that identifies objects with a Metadata property of type <see cref="List{T}"/>.
    /// </summary>
    public interface IHasTags
    {
        /// <summary>
        /// Set of values that you can attach to an object. This can be useful for searching.
        /// </summary>
        public List<string> Tags { get; set; }
    }
}
