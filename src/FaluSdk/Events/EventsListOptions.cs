using Falu.Core;
using System.Collections.Generic;

namespace Falu.Events
{
    /// <summary>Options for filtering and pagination of events.</summary>
    public record EventsListOptions : BasicListOptions
    {
        /// <summary>Filter options for <see cref="WebhookEvent{TObject}.Type"/> property.</summary>
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
