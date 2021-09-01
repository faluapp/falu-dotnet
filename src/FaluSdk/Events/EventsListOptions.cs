using Falu.Core;
using System.Collections.Generic;

namespace Falu.Events
{
    /// <summary>
    /// Options for filtering and pagination of events.
    /// </summary>
    public class EventsListOptions : BasicListOptions
    {
        /// <summary>
        /// Filter options for <code>type</code> property.
        /// </summary>
        public List<string>? Type { get; set; }

        /// <inheritdoc/>
        internal override IDictionary<string, string> PopulateQueryValues(IDictionary<string, string> dictionary)
        {
            base.PopulateQueryValues(dictionary);

            dictionary.AddIfNotNull("type", Type, ConvertStringList);

            return dictionary;
        }
    }
}
